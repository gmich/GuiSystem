using GuiSystem.Rendering;
using GuiSystem.Structure;
using GuiSystem.Style;
using Microsoft.Xna.Framework;

namespace GuiSystem.Elements
{
    public class BaseElement : IGuiElement
    {
        public string Group { get; set; } = "Dummy";

        public string Id { get; set; } = "Dummy";

        public Rectangle OccupiedScreenRectangle { get; set; }

        public virtual void HandleInput(Input.InputManager input) { }

        public virtual void Update(double timeDelta) { }

        public virtual void Render(RenderContext context, IStylingRule style)
        {
            var texture = style.BackgroundImage ?? context.Content.Textures["blank"];
            style.RenderMethod.Render(context.Batch, context.SafeArea, texture, style);
        }
    }
}
