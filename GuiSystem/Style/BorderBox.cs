using System;
using Microsoft.Xna.Framework;

namespace GuiSystem.View
{
    public class BorderBox : Box
    {
        public static BorderBox Thin
        {
            get
            {
                return Create(1);
            }
        }

        public static BorderBox Create(int thickness)
        {
            return new BorderBox
            {
                Top = thickness,
                Bottom = thickness,
                Left = thickness,
                Right = thickness
            };
        }
        public IBorderStyle Style { get; set; } = BorderStyle.None;

        public void Render(Rendering.RenderContext context)
        {
            Style.Render(context, this);
        }
    }
}
