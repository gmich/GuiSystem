using GuiSystem.Containers;
using GuiSystem.Elements;
using GuiSystem.Input;
using GuiSystem.Rendering;
using GuiSystem.Structure;
using GuiSystem.Style;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GuiSystem
{
    public class GuiService
    {
        private readonly ITree<IGuiElement> guiTree;
        private readonly InputManager inputManager;
        private readonly Renderer renderer;
        private readonly TreeTraverser traverser;

        public StyleService Style { get; }

        public GuiService(GraphicsDevice gfxDevice,
                          ContentManager content,
                          Func<Rectangle> viewportBounds,
                          Action<TreeBuilder> buildingAction)
        {
            var treeBuilder = TreeBuilder.Root(new WindowElement(viewportBounds));
            var spriteBatch = new SpriteBatch(gfxDevice);
            buildingAction(treeBuilder);
            guiTree = treeBuilder.Tree;
            inputManager = new InputManager();
            Style = new StyleService(guiTree);
            renderer = new Renderer(
                spriteBatch,
                new ContentContainer(content),
                Style.GetStyleByElement);
        }


        public void Update(double timeDelta)
        {
            inputManager.Flush();
            Style.Update(timeDelta);
        }

        public void Render()
        {
            renderer.Prepare(() =>
                traverser.Traverse(renderer.RenderElement));
        }
    }
}
