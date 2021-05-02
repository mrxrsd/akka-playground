using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using Core.Messages.Common;

namespace AsyncRequestResponse
{
    public class StorageActor :ReceiveActor
    {
        public StorageActor()
        {
            Receive<RequestFlow<HaveIngredients>>(m =>
            {
                if (m.Request.Flavour == "pepperoni")
                {
                    Context.Sender.Tell(Flow.Respond(new WeHaveAllIngrendients(), m ));
                }
                else
                {
                    Context.Sender.Tell(Flow.Respond(new MissingIngredients(), m));
                }
            });
        }
    }
}
