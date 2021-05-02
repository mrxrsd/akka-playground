using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Messages.Common
{
    public class WakeUp
    {
        public string Action { get; }

        public WakeUp(string action)
        {
            Action = action;
        }
    }
}
