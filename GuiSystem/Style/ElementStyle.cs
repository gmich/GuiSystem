using GuiSystem.Structure;
using System;
using System.Collections.Generic;

namespace GuiSystem.Style
{

    public class StyleService
    {

        private class StyleEntry
        {
            public IPriority Priority { get; }
            public IStylingRule Style { get; }
            public StyleEntry(IPriority priority, IStylingRule style)
            {
                Priority = priority;
                Style = style;
            }
        }

        private readonly Dictionary<IGuiElement, StyleEntry> styledElements = new Dictionary<IGuiElement, StyleEntry>();
        private static List<Action<double>> stylingrules = new List<Action<double>>();
        private readonly ITree<IGuiElement> guiTree;
        private readonly IStylingRule defaultRule = new StylingRule();

        public StyleService(ITree<IGuiElement> guiTree)
        {
            this.guiTree = guiTree;
        }


        private void ResolveSelector(ElementSelector selector, IStylingRule rule)
        {
            var selectedElements = selector.GetSelection(guiTree);

            foreach (var element in selectedElements)
            {
                if (styledElements.ContainsKey(element))
                {
                    if (selector.Priority.Amount > styledElements[element].Priority.Amount)
                    {
                        styledElements.Remove(element);
                    }
                    else continue;
                }
                styledElements.Add(element, new StyleEntry(selector.Priority, rule));
            }
        }

        public IStylingRule GetStyleByElement(IGuiElement element)
        {
            if (styledElements.ContainsKey(element))
            {
                return styledElements[element].Style;
            }
            return defaultRule;
        }

        public void Attach(ElementSelector selector, IStylingRule ruleSet)
        {
            ResolveSelector(selector, ruleSet);
        }

        public void Attach(ElementSelector selector, Action<StylingRule> ruleSet)
        {
            var style = new StylingRule();
            ResolveSelector(selector, style);
            ruleSet(style);
        }

        public void Attach(ElementSelector selector, Action<StylingRule, double> ruleSet, AnimationSpan span)
        {
            var style = new StylingRule();
            ResolveSelector(selector, style);
            stylingrules.Add((time) =>
            {
                ruleSet(style, time);
                span.Update(time);
            });
            span.OnAnimationEnd += (sender, args) => stylingrules.RemoveAt(stylingrules.Count - 1);
        }

        public void Update(double timeDelta)
        {
            stylingrules.ForEach(rule => rule.Invoke(timeDelta));
        }
    }
}
