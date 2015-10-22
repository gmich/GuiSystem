using GuiSystem.Rendering;
using GuiSystem.Style;
using Microsoft.Xna.Framework;

namespace GuiSystem.Structure
{
    public interface IGuiElement
    {
        string Id { get; set; }
        string Group { get; set; }

        Rectangle RenderRectangle { get; set; }

        void HandleInput(Input.InputManager input);

        void Update(double timeDelta);

        void Render(RenderContext context, IStylingRule style);
    }
}
