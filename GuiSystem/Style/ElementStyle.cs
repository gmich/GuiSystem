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
                AddElement(element, rule, selector.Priority);
            }
        }

        private void AddElement(IGuiElement element, IStylingRule rule, IPriority priority)
        {
            if (styledElements.ContainsKey(element))
            {
                if (priority.Amount > styledElements[element].Priority.Amount)
                {
                    styledElements[element].Style.Override(rule);
                }
                else
                {
                    styledElements[element].Style.Merge(rule);
                }
                return;
            }
            styledElements.Add(element, new StyleEntry(priority, rule));

        }

        public IStylingRule GetStyleByElement(IGuiElement element)
        {
            if (styledElements.ContainsKey(element))
            {
                return styledElements[element].Style;
            }
            return defaultRule;
        }

        public void Attach(IGuiElement element, IStylingRule ruleSet)
        {
            AddElement(element, ruleSet, SelectorPriority.Default);
        }

        public void Attach(IGuiElement element, Action<StylingRule> ruleSet)
        {
            var style = new StylingRule();
            AddElement(element, style, SelectorPriority.Default);
            ruleSet(style);
        }

        public void Attach(IGuiElement element, Action<StylingRule, double> ruleSet, AnimationSpan span)
        {
            var style = new StylingRule();
            AddElement(element, style, SelectorPriority.Default);
            stylingrules.Add((time) =>
            {
                ruleSet(style, time);
                span.Update(time);
            });
            span.OnAnimationEnd += (sender, args) => stylingrules.RemoveAt(stylingrules.Count - 1);
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
