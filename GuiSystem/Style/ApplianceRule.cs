using GuiSystem.Structure;
using System;

namespace GuiSystem.Style
{

    public interface IApplianceRule
    {
        Predicate<INode<IGuiElement>> DoesElementApply { get; }
    }

    public class ApplianceRule : IApplianceRule
    {
        public Predicate<INode<IGuiElement>> DoesElementApply { get; }

        public ApplianceRule(Predicate<INode<IGuiElement>> rule)
        {
            DoesElementApply = rule;
        }
    }
}
