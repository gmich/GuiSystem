using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiSystem.Structure
{

    public class TreeBuilder
    {
        public INode<IGuiElement> Tree { get; }
        public INode<IGuiElement> ActiveNode { get; private set; }
        public int Level { get; private set; } = 1;

        private TreeBuilder(IGuiElement root)
        {
            Tree = Node<IGuiElement>.CreateTree(root);
            ActiveNode = Tree;
        }

        public static TreeBuilder Root(IGuiElement rootElement)
        {
            return new TreeBuilder(rootElement);
        }

        public TreeBuilder Up(int levels = 1)
        {
            for (int level = 0; level < levels; level++)
            {
                ActiveNode = ActiveNode.Parent;
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
                ActiveNode = ActiveNode.DirectChildren.First();
                Level++;
            }
            return this;
        }

        public TreeBuilder Add(params IGuiElement[] elements)
        {
            foreach (var element in elements)
            {
                ActiveNode.AddChild(element);
            }
            return this;
        }
    }
}
