using GuiSystem.Containers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GuiSystem.Rendering
{
    public class RenderContext
    {
        public RenderContext(ContentContainer content, SpriteBatch batch, Rectangle bounds, Rectangle safeArea)
        {
            Content = content;
            Batch = batch;
            Bounds = bounds;
            SafeArea = safeArea;
        }

        public ContentContainer Content { get; }

        public SpriteBatch Batch { get; }

        public Rectangle Bounds { get;  }

        public Rectangle SafeArea { get; }
    }

}
