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

        public Renderer(SpriteBatch batch, ContentContainer content, Func<IGuiElement, IStylingRule> styleProvider)
        {
            this.batch = batch;
            this.content = content;
            this.styleProvider = styleProvider;

            content.Textures.Add("blank", cnt => new Texture2D(batch.GraphicsDevice, 1, 1));
        }

        private void RenderInSafeArea(Rectangle elementBounds, Action renderAction)
        {
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

            var alignmentContext = CalculateAlignmentContext(element);
            var renderRectangle = elementStyle.Alignment.CalculateSafeArea(parent.Data.RenderRectangle, elementStyle, alignmentContext);
            element.Data.RenderRectangle = renderRectangle;

            var context =
                 new RenderContext(
                     content,
                     batch,
                     parent.Data.RenderRectangle,
                     new Rectangle(0, 0, renderRectangle.Width, renderRectangle.Height)
                );

            RenderInSafeArea(renderRectangle, () => element.Data.Render(context, elementStyle));
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
