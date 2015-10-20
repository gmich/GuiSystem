using GuiSystem.GTerminal.View;
using GuiSystem.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GuiSystem.Style
{

    public interface IStylingRule
    {
        int? Width { get;  }
        int? Height { get;  }
        Texture2D BackgroundImage { get;  }
        Color BackgroundColor { get;  }
        Color TextColor { get;  }
        string Text { get;  }
        Box Padding { get;  }
        BorderBox Border { get;  }
        Box Margin { get;  }
        IAlignment Alignment { get;  }
    }
}
