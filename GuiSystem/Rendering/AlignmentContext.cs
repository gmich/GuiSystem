using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuiSystem.Rendering
{
    public class AlignmentContext
    {
        public AlignmentContext(Entry xAxis, Entry yAxis)
        {
            XAxis = xAxis;
            YAxis = yAxis;
        }

        public class Entry
        {
            public Entry(int size, int pixelsPerEntry)
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
}
