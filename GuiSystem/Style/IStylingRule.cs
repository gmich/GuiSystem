using GuiSystem.GTerminal.View;
using GuiSystem.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GuiSystem.Style
{

    public interface IStylingRule
    {
        int? X { get; }
        int? Y { get; }
        int? Width { get;  }
        int? Height { get;  }
        Vector2 Scale { get; }
        float Rotation { get; }
        Texture2D BackgroundImage { get;  }
        Color Color { get;  }
        Box Padding { get;  }
        BorderBox Border { get;  }
        Box Margin { get;  }
        IAlignment Alignment { get;  }

        RenderingMethod RenderMethod { get; set; }
        void Merge(IStylingRule other);
        void Override(IStylingRule other);

    }
}
