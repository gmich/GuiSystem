using GuiSystem.Containers;
using GuiSystem.Rendering;
using GuiSystem.Style;
using GuiSystem.Toolbox;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;

namespace GuiSystem.Structure
{
    public delegate void TreeRenderVisitor(
        INode<IGuiElement> current,
        INode<IGuiElement> parent,
        RenderContext context);

    public class TreeRenderTraverser
    {
        public ITree<IGuiElement> Tree { get; }
        private readonly Func<IGuiElement, IStylingRule> styleProvider;
        private readonly ContentContainer content;
        private readonly SpriteBatch batch;

        public TreeRenderTraverser(ITree<IGuiElement> tree,
            ContentContainer content,
            SpriteBatch batch,
            Func<IGuiElement, IStylingRule> styleProvider)
        {
            this.content = content;
            this.batch = batch;
            Tree = tree;
            this.styleProvider = styleProvider;
        }

        private void RenderInRectangle(Rectangle elementBounds, Action renderAction)
        {
            var renderingRectangle = batch.GraphicsDevice.ScissorRectangle;
            batch.GraphicsDevice.ScissorRectangle = elementBounds;
            renderAction();

            batch.GraphicsDevice.ScissorRectangle = renderingRectangle;
        }

        private void RenderElement(SpriteBatch batch, INode<IGuiElement> element, INode<IGuiElement> parent, RenderContext context)
        {
            var elementStyle = styleProvider(element.Data);
            var parentStyle = styleProvider(parent.Data);

            var parentRectangle = GetParentSize(parent);
            //var context =
            //    new RenderContext(content,
            //    batch,
            //    parentBoundaries,
            //    style.Alignment.CalculateSafeArea(parentBoundaries, style, context.AlignmentContext));

            //element.Render(context, style);

            Rectangle parentBounds;
            GetParentSize(element)
                .OnSuccess(result => parentBounds = new Rectangle(Point.Zero, result))
                .OnFailure(() => parentBounds = batch.GraphicsDevice.Viewport.Bounds);
        }

        /// <summary>
        /// Traverses the tree bottom-up to find the first defined width and height 
        /// </summary>
        public Operation<Point> GetParentSize(INode<IGuiElement> element, int? width = null, int? height = null)
        {
            var style = styleProvider(element.Data);

            int? elementWidth = width ?? style.Width;
            int? elementHeight = height ?? style.Height;
            if (style.Width == null && style.Height == null)
            {
                return (element.HasParent)
                ? GetParentSize(element.Parent, elementWidth, elementHeight)
                : Operation.Failed(new Point(elementWidth.Value, elementHeight.Value));
            }
            return Operation.Succeeded(new Point(elementWidth.Value, elementHeight.Value));
        }

        public void Traverse(TreeRenderVisitor visitor, RenderContext context)
        {
            RecursiveTraversingHelper(
                Tree.Root,
                null,
                visitor,
                context);
        }

        private void RecursiveTraversingHelper(
            INode<IGuiElement> node,
            INode<IGuiElement> parent,
            TreeRenderVisitor visitor,
            RenderContext context)
        {
            visitor(node, parent, context);
            var alignmentContext = CalculateAlignmentContext(node);

            foreach (var childNode in node.DirectChildren.Nodes)
            {
                RecursiveTraversingHelper(childNode, node, visitor, context);
            }
        }

        private AlignmentContext CalculateAlignmentContext(INode<IGuiElement> elementNode)
        {
            var childSiblings = elementNode.DirectChildren.Nodes.Select(child => child.Data);
            var parentStyle = styleProvider(elementNode.Data);

            int staticWidth = 0;
            int staticHeight = 0;
            int totalElements = 0;
            foreach (var element in childSiblings)
            {
                var style = styleProvider(element);
                staticWidth += style.Width ?? 0;
                staticHeight += style.Height ?? 0;
                totalElements++;
            }

            return new AlignmentContext(
                xAxis: new AlignmentContext.Entry(
                 parentStyle.Width.Value,
                 (parentStyle.Width.Value - staticWidth)
                    / childSiblings.Count(element => styleProvider(element).Width == null)),
                yAxis: new AlignmentContext.Entry(
                 parentStyle.Height.Value,
                 (parentStyle.Height.Value - totalElements)
                    / childSiblings.Count(element => styleProvider(element).Height == null)));
        }

    }
}
