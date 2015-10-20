using GuiSystem.Rendering;
using GuiSystem.Style;

namespace GuiSystem.Structure
{
    public interface IGuiElement
    {
        string Id { get; set; }
        string Group { get; set; }

        void HandleInput(Input.InputManager input);

        void Update(double timeDelta);

        void Render(RenderContext context, IStylingRule style);
    }
}
