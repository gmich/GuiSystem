using System;
using GuiSystem.Input;
using GuiSystem.Rendering;
using GuiSystem.Structure;
using GuiSystem.Style;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GuiSystem.Elements
{
    public class BaseElement : IGuiElement
    {
        public string Group { get; set; } = "Dummy";

        public string Id { get; set; } = "Dummy";

        public Rectangle RenderRectangle { get; set; }

        public void HandleInput(Input.InputManager input) { }

        public void Update(double timeDelta) { }

        public void Render(RenderContext context, IStylingRule style)
        {
            var texture = style.BackgroundImage ?? context.Content.Textures["blank"];
           // context.Batch.Draw(texture, null,context.SafeArea, style.Color, style.Rotation, Vector2.Zero, SpriteEffects.None, 0.0f); 
        }
    }
}
