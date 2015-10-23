using GuiSystem.Style;
using Microsoft.Xna.Framework;
using System;

namespace GuiSystem.Rendering
{

    public interface IAlignment
    {
        Rectangle CalculateSafeArea(Rectangle parentBoundaries, IStylingRule style, AlignmentContext context);
    }

    public class AutoAlignment : IAlignment
    {
        public Rectangle CalculateSafeArea(Rectangle parentBoundaries,IStylingRule style, AlignmentContext context)
        {
            var width = style.Width ?? parentBoundaries.Width;
            var height = style.Height ?? parentBoundaries.Height;
            var safeArea =  new Rectangle(
               parentBoundaries.X + (parentBoundaries.Width - context.XAxis.SpaceAvailable)
               , parentBoundaries.Y + (parentBoundaries.Height - context.YAxis.SpaceAvailable)
               , width
               , height);
            return safeArea;
        }
    }

    public sealed class Alignment
    {

        private static Lazy<AutoAlignment> autoAlignment = new Lazy<AutoAlignment>();
        public static IAlignment Auto
        {
            get { return autoAlignment.Value; }
        }
    }
}
