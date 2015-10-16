using System;
using System.Collections.Generic;
using System.Linq;

namespace GuiSystem.Structure
{
    public class ElementSelector
    {
        private readonly Predicate<IGuiElement> predicate;
        public Func<ITree<IGuiElement>, IEnumerable<IGuiElement>> Select { get; private set; }

        private ElementSelector(Predicate<IGuiElement> predicate)
        {
            this.predicate = predicate;
        }

        private ElementSelector(Func<ITree<IGuiElement>, IEnumerable<IGuiElement>> Select)
        {
            this.Select = Select;
        }

        public static ElementSelector For(Predicate<IGuiElement> predicate)
        {
            return new ElementSelector(predicate);
        }

        public ElementSelector All
        {
            get
            {
                Select = tree => tree.Nodes
                                     .Where(node => predicate(node.Data))
                                     .Select(node => node.Data);
                return this;
            }
        }

        public ElementSelector TakeMany(Func<INode<IGuiElement>, IEnumerable<INode<IGuiElement>>> selector)
        {
            Select = tree =>
            {
                var nodes = tree.Nodes.Where(node => predicate(node.Data));
                var elements = new List<IGuiElement>();
                elements.AddRange(nodes.Select(node => selector(node))
                                       .SelectMany(projectedNode => projectedNode.Select(node => node.Data)));
                return elements;
            };
            return this;
        }


        public ElementSelector TakeOnly(Func<INode<IGuiElement>, INode<IGuiElement>> selector)
        {
            Select = tree =>
            {
                var nodes = tree.Nodes.Where(node => predicate(node.Data));
                var elements = new List<IGuiElement>();
                elements.AddRange(nodes.Select(node => selector(node))
                                       .Select(projectedNode => projectedNode.Data));

                return elements;
            };
            return this;
        }

        public ElementSelector CreateSelection(params ISelector[] selectors)
        {
            Select = tree =>
            {
                var nodes = tree.Nodes.Where(node => predicate(node.Data));
                var elements = new List<IGuiElement>();
                foreach (var selector in selectors)
                {
                    var select = selector as GetMany;
                    if (select == null)
                    {
                        var individualSelector = (GetEach)selector;
                        elements.AddRange(nodes.Select(node => individualSelector.PerformSelection(node))
                                     .Select(projectedNode => projectedNode.Data));
                    }
                    else
                    {
                        elements.AddRange(nodes.Select(node => select.PerformSelection(node))
                                   .SelectMany(projectedNode => projectedNode.Select(node => node.Data)));
                    }
                }
                return elements;
            };
            return this;
        }

    }
}
