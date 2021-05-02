using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;

namespace AsyncRequestResponse
{
    public class KitchenActor : ReceiveActor
    {
        public KitchenActor()
        {
            ReceiveAny(m =>
            {
                var r = new Random();

                Context.Sender.Tell($"order accepted - waiting time: {r.Next(5,25)} mins");
            });
        }
    }
}
