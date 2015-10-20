using System;
using GuiSystem.Structure;
using Microsoft.Xna.Framework;
using GuiSystem.Style;

namespace GuiSystem.Rendering
{
    public class Renderer : IRenderer
    {
        //NOTICE: moved it to TreeRenderTraverser.cs

        //private void RenderInRectangle(Rectangle elementBounds, Action renderAction)
        //{
        //    var renderingRectangle = context.Batch.GraphicsDevice.ScissorRectangle;
        //    context.Batch.GraphicsDevice.ScissorRectangle = elementBounds;
        //    renderAction();

        //    context.Batch.GraphicsDevice.ScissorRectangle = renderingRectangle;
        //}

        //public void RenderElement(IGuiElement element, IStylingRule style, Rectangle parentBoundaries, RenderContext context)
        //{
        //    context.SafeArea = style.Alignment.CalculateSafeArea(parentBoundaries, style, context.AlignmentContext);
        //    context.Bounds = parentBoundaries;
        //    element.Render(context, style);
        //}

    }
}
