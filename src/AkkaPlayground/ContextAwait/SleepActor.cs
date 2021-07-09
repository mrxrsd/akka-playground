using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace ContextAwait
{


    public class Sleep
    {
        public TimeSpan Ts { get; }

        public Sleep(TimeSpan ts)
        {
            Ts = ts;
        }
    }

    public class SleepActor : ReceiveActor
    {
        public SleepActor()
        {
            ReceiveAsync<Sleep>(async m =>
            {
                await Task.Delay(m.Ts);
                Context.Sender.Tell(true);
            });
        }
    }
}
