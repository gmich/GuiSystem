using System;
using System.Collections.Generic;
using System.Linq;

namespace GuiSystem.Structure
{
    public delegate void TreeVisitor<TNodeData>(
        TNodeData current,
        TNodeData parent,
        IEnumerable<TNodeData> siblings);

    public delegate void PredicateRulke<TNodeData>(
        TNodeData current,
        TNodeData parent,
        IEnumerable<TNodeData> siblings);

    public class TreeTraverser<TNodeData>
    {
        public ITree<TNodeData> Tree { get; }

        public TreeTraverser(ITree<TNodeData> tree)
        {
            Tree = tree;
        }

        public void Traverse(TreeVisitor<TNodeData> visitor)
        {
            RecursiveTraversingHelper(
                Tree.Root,
                default(TNodeData),
                Enumerable.Empty<TNodeData>(),
                visitor);
        }

        private void RecursiveTraversingHelper(
            INode<TNodeData> node,
            TNodeData parent,
            IEnumerable<TNodeData> siblings,
            TreeVisitor<TNodeData> visitor)
        {
            visitor(node.Data, parent, siblings);
            var childSiblings = node.DirectChildren.Nodes.Select(child => child.Data);
            foreach (var childNode in node.DirectChildren.Nodes)
            {
                RecursiveTraversingHelper(childNode, node.Data, childSiblings, visitor);
            }
        }

    }
}
