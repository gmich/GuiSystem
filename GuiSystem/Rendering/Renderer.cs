using GuiSystem.Containers;
using GuiSystem.Structure;
using GuiSystem.Style;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;

namespace GuiSystem.Rendering
{
    public class Renderer
    {
        private readonly SpriteBatch batch;
        private readonly ContentContainer content;
        private readonly Func<IGuiElement, IStylingRule> styleProvider;
        private readonly RasterizerState rasterizer = new RasterizerState() { ScissorTestEnable = true };
        private IGuiElement previousParent;
        private AlignmentContext alignmentContext;
        private readonly RenderContext renderContext;

        public Renderer(SpriteBatch batch, ContentContainer content, Func<IGuiElement, IStylingRule> styleProvider)
        {
            this.batch = batch;
            this.content = content;
            this.styleProvider = styleProvider;
            renderContext = new RenderContext(content, batch);
            content.Textures.Add("blank", cnt => new Texture2D(batch.GraphicsDevice, 1, 1));
        }

        private void RenderInSafeArea(Rectangle elementBounds, Action renderAction)
        {
            previousParent = null;
            var renderingRectangle = batch.GraphicsDevice.ScissorRectangle;
            batch.GraphicsDevice.ScissorRectangle = elementBounds;
            renderAction();

            batch.GraphicsDevice.ScissorRectangle = renderingRectangle;
        }

        public void Prepare(Action doRendering)
        {
            batch.Begin(SpriteSortMode.BackToFront,
                        BlendState.AlphaBlend,
                        rasterizerState: rasterizer);
            doRendering();
            batch.End();
        }

        public void RenderElement(INode<IGuiElement> element, INode<IGuiElement> parent)
        {
            var elementStyle = styleProvider(element.Data);
            alignmentContext = (previousParent == parent) ?
                alignmentContext : CalculateAlignmentContext(element);

            var renderRectangle = elementStyle.Alignment.CalculateSafeArea(parent.Data.OccupiedScreenRectangle, elementStyle, alignmentContext);
            element.Data.OccupiedScreenRectangle = renderRectangle;

            renderContext.SafeArea =
                     new Rectangle(0, 0, renderRectangle.Width, renderRectangle.Height);
  
            RenderInSafeArea(renderRectangle, () => element.Data.Render(renderContext, elementStyle));
            elementStyle.Border.Render(renderContext);
            alignmentContext.Update(element.Data);
        }

        private AlignmentContext CalculateAlignmentContext(INode<IGuiElement> elementNode)
        {
            var childSiblings = elementNode.DirectChildren.Nodes.Select(child => child.Data);
            var parent = styleProvider(elementNode.Parent.Data)
                        .GetSafeArea(elementNode.Parent.Data.OccupiedScreenRectangle);

            int staticWidth = 0;
            int staticHeight = 0;
            int totalElements = 0;
            foreach (var element in childSiblings)
            {
                var style = styleProvider(element);
                staticWidth += style.TotalWidth();
                staticHeight += style.TotalHeight();
                totalElements++;
            }

            return new AlignmentContext(
                xAxis: new AlignmentContext.Entry(
                 parent.Width,
                 (parent.Width - staticWidth)
                    / childSiblings.Count(element => styleProvider(element).Width == null)),
                yAxis: new AlignmentContext.Entry(
                parent.Height,
                 (parent.Height - staticHeight)
                    / childSiblings.Count(element => styleProvider(element).Height == null)));
        }

    }
}
