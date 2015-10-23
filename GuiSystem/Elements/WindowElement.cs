using GuiSystem.Rendering;
using GuiSystem.Structure;
using GuiSystem.Style;
using Microsoft.Xna.Framework;
using System;

namespace GuiSystem.Elements
{
    public class WindowElement : IGuiElement
    {
        private readonly Func<Rectangle> viewPort;

        public WindowElement(Func<Rectangle> viewPort)
        {
            this.viewPort = viewPort;
        }

        public string Group { get; set; } = "Window";

        public string Id { get; set; } = "Window";

        public Rectangle OccupiedScreenRectangle { get { return viewPort(); } set{} }

        public void HandleInput(Input.InputManager input) { }

        public void Update(double timeDelta) { }

        public void Render(RenderContext context, IStylingRule style) { }
    }
}
