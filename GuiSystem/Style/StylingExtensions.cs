using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuiSystem.Style
{
    public static class StylingExtensions
    {
        public static Rectangle GetSafeArea(this IStylingRule style, Rectangle screenOccupiedRectangle)
        {
            return new Rectangle(screenOccupiedRectangle.Left + (style.Padding.Left + style.Border.Left + style.Margin.Left),
                                 screenOccupiedRectangle.Top + (style.Padding.Top + style.Border.Top + style.Margin.Top),
                                 screenOccupiedRectangle.Right - (style.Padding.Right + style.Border.Right + style.Margin.Right),
                                 screenOccupiedRectangle.Bottom - (style.Padding.Bottom + style.Border.Bottom + style.Margin.Bottom));
        }

        public static Rectangle GetInteractiveArea(this IStylingRule style, Rectangle screenOccupiedRectangle)
        {
            return new Rectangle(screenOccupiedRectangle.Left + (style.Padding.Left + style.Border.Left),
                                 screenOccupiedRectangle.Top + (style.Padding.Top + style.Border.Top),
                                 screenOccupiedRectangle.Right - (style.Padding.Right + style.Border.Right),
                                 screenOccupiedRectangle.Bottom - (style.Padding.Bottom + style.Border.Bottom));
        }

        public static int TotalWidth(this IStylingRule style)
        {
            return style.Width ?? 0 + (style.Padding.Left + style.Padding.Right
                    + style.Border.Left + style.Border.Right
                    + style.Margin.Left + style.Margin.Right);
        }

        public static int TotalHeight(this IStylingRule style)
        {
            return style.Height ?? 0 + (style.Padding.Top + style.Padding.Bottom
                    + style.Border.Top + style.Border.Bottom
                    + style.Margin.Top + style.Margin.Bottom);
        }
    }
}
