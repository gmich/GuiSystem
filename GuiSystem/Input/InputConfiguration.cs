using System;
using System.Collections.Generic;

namespace GuiSystem.Input
{
    public class InputConfiguration
    {
        public Dictionary<Func<bool>, Action> Entries { get; }
                
        public InputConfiguration(Dictionary<Func<bool>,Action> inputEventAndAction)
        {
            Entries = inputEventAndAction;
        }

        public void InvokeApplicableActions()
        {
            foreach(var entry in Entries)
            {
                if(entry.Key())
                {
                    entry.Value.Invoke();
                }
            }
        }

    }
}
