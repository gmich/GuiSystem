using System;
using System.Collections.Generic;

namespace GuiSystem.Structure
{ 

    public interface ISelector
    {
        ISelectorPriority Priority { get; }
    }

    public class GetMany : ISelector
    {
        public Func<INode<IGuiElement>, IEnumerable<INode<IGuiElement>>> PerformSelection { get; }
        public ISelectorPriority Priority { get; } = SelectorPriority.Default;
        private GetMany(Func<INode<IGuiElement>, IEnumerable<INode<IGuiElement>>> collectionSelector)
        {
            PerformSelection = collectionSelector;
        }

        public static GetMany Of(Func<INode<IGuiElement>, IEnumerable<INode<IGuiElement>>> collectionSelector)
        {
            return new GetMany(collectionSelector);
        }
    }

    public class GetEach : ISelector
    {
        public Func<INode<IGuiElement>, INode<IGuiElement>> PerformSelection { get; }

        public ISelectorPriority Priority { get; } = SelectorPriority.Default;

        private GetEach(Func<INode<IGuiElement>, INode<IGuiElement>> individualSelector)
        {
            PerformSelection = individualSelector;
        }
        public static GetEach Of(Func<INode<IGuiElement>, INode<IGuiElement>> individualSelector)
        {
            return new GetEach(individualSelector);
        }
    }

}

