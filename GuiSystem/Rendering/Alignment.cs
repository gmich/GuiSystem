using GuiSystem.Structure;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace GuiSystem.Rendering
{

    public class AlignmentContext
    {
        public AlignmentContext(Entry xAxis, Entry yAxis)
        {
            XAxis = xAxis;
            YAxis = yAxis;
        }

        public struct Entry
        {
            public Entry(int size, int pixelsPerEntry )
            {
                SpaceAvailable = size;
                Size = size;
                PixelsPerEntry = pixelsPerEntry;
                ItemsIterated = 0;
            }

            public int SpaceAvailable { get; set; }
            public int PixelsPerEntry { get; set; }
            public int Size { get; set; }
            public int ItemsIterated { get; set; }
        }

        public Entry XAxis { get; }
        public Entry YAxis { get; }
    }


    public interface IAlignment
    {
        Rectangle CalculateSafeArea(IGuiElement view, Rectangle parentBoundaries, List<Rectangle> siblingBoundaries);
    }

    public class LeftAlignment : IAlignment
    {
        public Rectangle CalculateSafeArea(IGuiElement view, Rectangle parentBoundaries, List<Rectangle> siblingBoundaries)
        {
           // var area = view.Margin.Bounds;

            var startingPoint = new Point(parentBoundaries.Left, parentBoundaries.Top);

            siblingBoundaries.Where(rect => rect.Contains(startingPoint));
            return Rectangle.Empty;
        }
    }

    public sealed class Alignment
    {
        public static IAlignment Auto
        {
            get { return null; }
        }
    }
}
