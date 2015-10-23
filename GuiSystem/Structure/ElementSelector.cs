using System;
using System.Collections.Generic;
using System.Linq;

namespace GuiSystem.Structure
{
    public class ElementSelector
    {
        private readonly Predicate<IGuiElement> predicate;
        public Func<INode<IGuiElement>, IEnumerable<IGuiElement>> GetSelection { get; private set; }
        public IPriority Priority { get; }

        private ElementSelector(Predicate<IGuiElement> predicate, IPriority priority)
        {
            Priority = priority;
            this.predicate = predicate;
            GetSelection = tree => tree.Nodes
                                 .Where(node => predicate(node.Data))
                                 .Select(node => node.Data);
        }

        public static ElementSelector By(Predicate<IGuiElement> predicate)
        {
            return new ElementSelector(predicate, SelectorPriority.Default);
        }

        public static ElementSelector ByID(Predicate<string> predicate)
        {
            return new ElementSelector(element => predicate(element.Id), SelectorPriority.Default);
        }

        public static ElementSelector ByGroup(Predicate<string> predicate)
        {
            return new ElementSelector(element=> predicate(element.Group),SelectorPriority.Default);
        }

        public ElementSelector TakeMany(Func<INode<IGuiElement>, IEnumerable<INode<IGuiElement>>> selector)
        {
            GetSelection = tree =>
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
            GetSelection = tree =>
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
            GetSelection = tree =>
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
