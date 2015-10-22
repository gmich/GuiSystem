using System;
using GuiSystem.Input;
using GuiSystem.Rendering;
using GuiSystem.Structure;
using GuiSystem.Style;
using Microsoft.Xna.Framework;

namespace GuiSystem.Elements
{
    public class TextElement : IGuiElement
    {
        private readonly Func<string> text;
        public TextElement(Func<string> text)
        {
            this.text = text;
        }

        public string Group { get; set; } = "Dummy";

        public string Id { get; set; } = "Dummy";

        public Rectangle RenderRectangle { get; set; }

        public void HandleInput(Input.InputManager input) { }

        public void Update(double timeDelta) { }

        public void Render(RenderContext context, IStylingRule style)
        {
            //var texture = style.BackgroundImage ?? context.Content.Textures["blank"];
            //context.Batch.Draw(texture, context.SafeArea, style.Color);
        }
    }
}
