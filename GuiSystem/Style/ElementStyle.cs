using GuiSystem.Structure;
using System;
using System.Collections.Generic;

namespace GuiSystem.Style
{
    public class ElementStyle
    {
        private readonly Dictionary<IGuiElement, IStylingRule> styledElements = new Dictionary<IGuiElement, IStylingRule>();
        private static List<Action<double>> stylingrules = new List<Action<double>>();
        private readonly ITree<IGuiElement> guiTree;

        public ElementStyle(ITree<IGuiElement> guiTree)
        {
            this.guiTree = guiTree;
        }

        public void Attatch(ElementSelector selector, Action<StylingRule, double> ruleSet, AnimationSpan span)
        {
            var rule = new StylingRule();
            stylingrules.Add((time) =>
            {
                ruleSet(rule, time);
                span.Update(time);
            });
            span.OnAnimationEnd += (sender, args) => stylingrules.RemoveAt(stylingrules.Count - 1);
        }

        public void Update(double timeDelta)
        {
            stylingrules.ForEach(rule => rule.Invoke(timeDelta));
        }
    }

    public class AnimationSpan
    {
        private readonly TimeSpan timeSpan;
        private double timePassed;
        public event EventHandler OnAnimationEnd;

        public AnimationSpan(TimeSpan timeSpan)
        {
            this.timeSpan = timeSpan;
        }

        public void Update(double timeDelta)
        {
            timePassed += timeDelta;

            if (timePassed >= timeSpan.TotalSeconds)
            {
                OnAnimationEnd?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
