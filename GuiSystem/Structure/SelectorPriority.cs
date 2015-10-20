namespace GuiSystem.Structure
{
    public class SelectorPriority : IPriority
    {
        public SelectorPriority(byte priority)
        {
            Amount = priority;
        }

        public byte Amount { get; }

        public static SelectorPriority Default { get { return new SelectorPriority(0); } }
    }
}
