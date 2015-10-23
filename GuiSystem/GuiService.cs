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
    public delegate void TreeVisitor(
      INode<IGuiElement> current,
      INode<IGuiElement> parent);

    public class GuiService
    {
        private readonly ITree<IGuiElement> guiTree;
        private readonly InputManager inputManager;
        private Renderer renderer;
        public StyleService Style { get; }

        public GuiService(GraphicsDeviceManager gfxManager,
                          ContentManager content,
                          Func<Rectangle> viewportBounds,
                          Action<TreeBuilder> buildingAction)
        {
            var treeBuilder = TreeBuilder.Root(new WindowElement(viewportBounds));
            buildingAction(treeBuilder);
            guiTree = treeBuilder.Tree;
            inputManager = new InputManager();
            Style = new StyleService(guiTree);

            gfxManager.DeviceCreated += (sender, args) =>
            {
               var spriteBatch = new SpriteBatch(gfxManager.GraphicsDevice);
               renderer = new Renderer(
                    spriteBatch,
                    new ContentContainer(content),
                    Style.GetStyleByElement);
            };
        }

        public void Traverse(TreeVisitor visitor)
        {
            RecursiveTraversingHelper(
                guiTree.Root,
                guiTree.Root,
                visitor);
        }

        private void RecursiveTraversingHelper(
            INode<IGuiElement> node,
            INode<IGuiElement> parent,
            TreeVisitor visitor)
        {
            visitor(node, parent);
            foreach (var childNode in node.DirectChildren.Nodes)
            {
                RecursiveTraversingHelper(childNode, node, visitor);
            }
        }

        public void Update(double timeDelta)
        {
            inputManager.Flush();
            Style.Update(timeDelta);
        }

        public void Render()
        {
            renderer.Prepare(() =>
                Traverse(renderer.RenderElement));
        }
    }
}
