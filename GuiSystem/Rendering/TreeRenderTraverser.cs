using GuiSystem.Rendering;
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
        private readonly Func<IGuiElement, Style.IStylingRule> styleGetter;

        public TreeRenderTraverser(ITree<IGuiElement> tree, Func<IGuiElement,Style.IStylingRule> styleGetter)
        {
            Tree = tree;
            this.styleGetter = styleGetter;
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
            context.AlignmentContext = CalculateAlignmentContext(node);

            foreach (var childNode in node.DirectChildren.Nodes)
            {
                RecursiveTraversingHelper(childNode, node, visitor, context);
            }
        }

        private AlignmentContext CalculateAlignmentContext(INode<IGuiElement> elementNode)
        {
            var childSiblings = elementNode.DirectChildren.Nodes.Select(child => child.Data);
            var parentStyle = styleGetter(elementNode.Data);

            int staticWidth = 0;
            int staticHeight = 0;
            int totalElements = 0;
            foreach(var element in childSiblings)
            {
                var style = styleGetter(element);
                staticWidth += style.Width ?? 0;
                staticHeight += style.Height ?? 0;
                totalElements++;
            }

            return new AlignmentContext(
                xAxis: new AlignmentContext.Entry(
                 parentStyle.Width.Value,
                 (parentStyle.Width.Value - staticWidth)
                    / childSiblings.Count(element => styleGetter(element).Width == null)),
                yAxis: new AlignmentContext.Entry(
                 parentStyle.Height.Value,
                 (parentStyle.Height.Value - totalElements)
                    / childSiblings.Count(element => styleGetter(element).Height == null)));

        }

    }
}
