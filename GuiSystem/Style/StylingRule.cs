using GuiSystem.GTerminal.View;
using GuiSystem.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GuiSystem.Style
{
    public class StylingRule : IStylingRule
    {
        public Func<int?> XProvider { get; set; } = () => null;
        public Func<int?> YProvider { get; set; } = () => null;
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
        public Func<IAlignment> AlignmentProvider { get; set; } = () => Rendering.Alignment.Auto;

        public int? X { get { return XProvider(); } }
        public int? Y { get { return YProvider(); } }
        public int? Width { get { return WidthProvider(); } }
        public int? Height { get { return HeightProvider(); } }
        public Texture2D BackgroundImage { get { return BackgroundImageProvider(); } }
        public SpriteFont Font { get { return FontProvider(); } }
        public Color Color { get { return BackgroundColorProvider(); } }
        public Color TextColor { get { return TextColorProvider(); } }
        public string Text { get { return TextProvider(); } }
        public Box Padding { get { return PaddingProvider(); } }
        public BorderBox Border { get { return BorderProvider(); } }
        public Box Margin { get { return MarginProvider(); } }
        public IAlignment Alignment { get { return AlignmentProvider(); } }

        public void Merge(IStylingRule other)
        {
            JoinHelper(this, other as StylingRule);
        }

        private void JoinHelper(StylingRule dominantRule, StylingRule rule)
        {
            if (dominantRule == null || rule == null)
            {
                throw new NotSupportedException();
            }
            XProvider = dominantRule.XProvider ?? rule.XProvider;
            YProvider = dominantRule.YProvider ?? rule.YProvider;
            WidthProvider = dominantRule.WidthProvider ?? rule.WidthProvider;
            HeightProvider = dominantRule.HeightProvider ?? HeightProvider;
            BackgroundImageProvider = dominantRule.BackgroundImageProvider ?? rule.BackgroundImageProvider;
            FontProvider = dominantRule.FontProvider ?? rule.FontProvider;
            BackgroundColorProvider = dominantRule.BackgroundColorProvider ?? rule.BackgroundColorProvider;
            TextColorProvider = dominantRule.TextColorProvider ?? rule.TextColorProvider;
            TextProvider = dominantRule.TextProvider ?? rule.TextProvider;
            PaddingProvider = dominantRule.PaddingProvider ?? rule.PaddingProvider;
            BorderProvider = dominantRule.BorderProvider ?? rule.BorderProvider;
            MarginProvider = dominantRule.MarginProvider ?? rule.MarginProvider;
            AlignmentProvider = dominantRule.AlignmentProvider ?? rule.AlignmentProvider;
        }

        public void Override(IStylingRule other)
        {
            JoinHelper(other as StylingRule, this);
        }
    }
}
