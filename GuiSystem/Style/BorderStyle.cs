using GuiSystem.Rendering;
using System;

namespace GuiSystem.View
{

    public interface IBorderStyle
    {
        void Render(RenderContext context, BorderBox border);
    }

    public class BorderStyle : IBorderStyle
    {
        private readonly Action<RenderContext, BorderBox> renderAction;

        public BorderStyle(Action<RenderContext, BorderBox> renderAction)
        {
            this.renderAction = renderAction;
        }

        public static BorderStyle None
        {
            get
            {
                return new BorderStyle((context, batch) => { });
            }
        }

        public void Render(RenderContext context, BorderBox border)
        {
            renderAction(context, border);
        }
    }
}
