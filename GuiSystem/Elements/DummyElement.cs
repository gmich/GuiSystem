using GuiSystem.Rendering;
using GuiSystem.Structure;
using GuiSystem.Style;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuiSystem.Elements
{
    public class DummyElement : IGuiElement
    {
        public string Group { get; set; } = "Dummy";

        public string Id { get; set; } = "Dummy";

        public Rectangle RenderRectangle { get; set; }

        public void HandleInput(Input.InputManager input) { }

        public void Update(double timeDelta) { }

        public void Render(RenderContext context, IStylingRule style) { }
    }
}
