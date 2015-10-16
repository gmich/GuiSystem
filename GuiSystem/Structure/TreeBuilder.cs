using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiSystem.Structure
{

    public class TreeBuilder
    {
        public ITree<IGuiElement> Tree { get; } = NodeTree<IGuiElement>.NewTree();
        public INode<IGuiElement> ActiveNode { get; private set; }
        public int Level { get; private set; } = 1;

        private TreeBuilder(IGuiElement root)
        {
            ActiveNode = Tree.AddChild(root);
        }

        public static TreeBuilder Root(IGuiElement rootElement)
        {
            return new TreeBuilder(rootElement);
        }

        public TreeBuilder Up(int levels = 1)
        {
            for (int level = 0; level < levels; level++)
            {
                SetActiveNode(ActiveNode.Parent, "Up");
                Level--;
            }
            return this;
        }

        public TreeBuilder Down(IGuiElement element)
        {
            Level++;
            ActiveNode = ActiveNode.AddChild(element);
            return this;
        }

        public TreeBuilder Down(int levels = 1)
        {
            for (int level = 0; level < levels; level++)
            {
                SetActiveNode(ActiveNode.Child, "Down");
                Level++;
            }
            return this;
        }

        public TreeBuilder Previous(int nodeCount = 1)
        {
            for (int node = 0; node < nodeCount; node++)
            {
                SetActiveNode(ActiveNode.Previous, "Left");
            }
            return this;
        }

        public TreeBuilder Next(int nodeCount = 1)
        {
            for (int node = 0; node < nodeCount; node++)
            {
                SetActiveNode(ActiveNode.Next, "Right");

            }
            return this;
        }

        public TreeBuilder Add(params IGuiElement[] elements)
        {
            foreach (var element in elements)
            {
                ActiveNode = ActiveNode.Add(element);
            }
            return this;
        }

        private void SetActiveNode(INode<IGuiElement> newActiveNode, string errorMessage)
        {
            if (newActiveNode == null)
            {
                throw new Exception(errorMessage);
            }
            ActiveNode = newActiveNode;

            new TreeBuilder(null).Add(null).Previous
        }
    }
}
