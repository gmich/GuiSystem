using GuiSystem.Containers;
using Microsoft.Xna.Framework.Graphics;

namespace GuiSystem.Rendering
{
    public class RenderContext
    {
        public RenderContext(ContentContainer content, SpriteBatch batch)
        {
            Content = content;
            Batch = batch;
        }
        public ContentContainer Content { get; }
        public SpriteBatch Batch { get; }
    }

}
