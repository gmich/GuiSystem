using GuiSystem.Rendering;
using GuiSystem.Style;
using System;
using System.Linq;

namespace GuiSystem.Structure
{
    public delegate void TreeVisitor(
        INode<IGuiElement> current,
        INode<IGuiElement> parent);

    public class TreeTraverser
    {
        public ITree<IGuiElement> Tree { get; }
        private readonly Func<IGuiElement, IStylingRule> styleProvider;

        public TreeTraverser(ITree<IGuiElement> tree)
        {
            Tree = tree;
        }
        
        public void Traverse(TreeVisitor visitor)
        {
            RecursiveTraversingHelper(
                Tree.Root,
                Tree.Root,
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
        
    }
}
