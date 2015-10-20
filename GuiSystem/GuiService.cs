using GuiSystem.Elements;
using GuiSystem.Input;
using GuiSystem.Rendering;
using GuiSystem.Structure;
using GuiSystem.Style;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GuiSystem
{
    public class GuiService
    {
        private readonly InputManager inputManager;
        private readonly IRenderer renderer;
        private readonly ITree<IGuiElement> guiTree;
        public StyleService Style { get; }

        public GuiService(Action<TreeBuilder> buildingAction)
        {
            var treeBuilder = TreeBuilder.Root(new WindowElement());

            buildingAction(treeBuilder);
            guiTree = treeBuilder.Tree;
            renderer = new Renderer();
            inputManager = new InputManager();
            Style = new StyleService(guiTree);
        }


        public void Update(double timeDelta)
        {
            inputManager.Flush();
            Style.Update(timeDelta);
        }

        public void Render(SpriteBatch batch)
        {
            
        }
    }
}
