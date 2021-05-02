using System;
using System.Collections.Generic;
using System.Text;

namespace AsyncRequestResponse
{
    public class AskPizza
    {
        public string Flavour { get; }

        public AskPizza(string flavour)
        {
            Flavour = flavour;
        }
    }
}
