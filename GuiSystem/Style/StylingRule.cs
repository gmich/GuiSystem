using GuiSystem.GTerminal.View;
using GuiSystem.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GuiSystem.Style
{
    public class StylingRule : IStylingRule
    {
        public Func<int?> WidthProvider { get; set; } = () => null;
        public Func<int?> HeightProvider { get; set; } = () => null;
        public Func<Texture2D> BackgroundImageProvider { get; set; } = () => null;
        public Func<SpriteFont> FontProvider { get; set; } = () => null;
        public Func<Color> BackgroundColorProvider { get; set; } = () => Color.White;
        public Func<Color> TextColorProvider { get; set; } = () => Color.Black;
        public Func<string> TextProvider { get; set; } = () => null;
        public Func<Box> PaddingProvider { get; set; } = () => Box.Empty;
        public Func<BorderBox> BorderProvider { get; set; } = () => BorderBox.Thin;
        public Func<Box> MarginProvider { get; set; } = () => Box.Empty;
        public Func<IAlignment> PositionProvider { get; set; } = () => Rendering.Alignment.Auto;

        public int? Width { get { return WidthProvider(); } }
        public int? Height { get { return HeightProvider(); } }
        public Texture2D BackgroundImage { get { return BackgroundImageProvider(); } }
        public SpriteFont Font { get { return FontProvider(); } }
        public Color BackgroundColor { get { return BackgroundColorProvider(); } }
        public Color TextColor { get { return TextColorProvider(); } }
        public string Text { get { return TextProvider(); } }
        public Box Padding { get { return PaddingProvider(); } }
        public BorderBox Border { get { return BorderProvider(); } }
        public Box Margin { get { return MarginProvider(); } }
        public IAlignment Alignment { get { return PositionProvider(); } }
    }
}
