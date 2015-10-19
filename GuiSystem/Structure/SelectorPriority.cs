namespace GuiSystem.Structure
{
    public class SelectorPriority : ISelectorPriority
    {
        public SelectorPriority(byte priority)
        {
            Priority = priority;
        }

        public byte Priority { get; }

        public static SelectorPriority Default { get { return new SelectorPriority(0); } }
    }
}
