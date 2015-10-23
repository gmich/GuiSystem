using GuiSystem.Style;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GuiSystem.Rendering
{
    public class RenderingMethod
    {
        public Action<SpriteBatch, Rectangle, Texture2D, IStylingRule> Render { get; }
        public RenderingMethod(Action<SpriteBatch, Rectangle, Texture2D, IStylingRule> render)
        {
            Render = render;
        }

        private static Lazy<RenderingMethod> byPosition = new Lazy<RenderingMethod>(
            () => new RenderingMethod((batch, area, texture, rule) =>
            {
                batch.Draw(texture, new Vector2(area.X, area.Y), rule.Color);
            }));

        public static RenderingMethod ByPosition
        {
            get { return byPosition.Value; }
        }

        private static Lazy<RenderingMethod> asBox = new Lazy<RenderingMethod>(
            () => new RenderingMethod((batch, area, texture, rule) =>
            {
                batch.Draw(texture, area, rule.Color);
            }));
        public static RenderingMethod AsBox
        {
            get { return asBox.Value; }
        }

    }
}
