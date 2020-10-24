using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorHandling
{
    public class Command
    {
        public string Destiny { get; }
        public string Msg { get; }

        public Command(string destiny, string msg)
        {
            Destiny = destiny;
            Msg = msg;
        }
    }
}
