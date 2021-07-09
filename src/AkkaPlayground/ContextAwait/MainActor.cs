using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace ContextAwait
{
    public class Hi
    {

    }
    public class MainActor : ReceiveActor
    {
        public MainActor()
        {
            Receive<Hi>(HiHandler);
        }

        private void HiHandler(Hi arg)
        {
            var sleepActor = Context.ActorOf(Props.Create<SleepActor>());
            var self = Context.Self;
            sleepActor.Ask(new Sleep(TimeSpan.FromSeconds(2)))
                      .ContinueWith(f =>
            {
                Console.WriteLine("Hi");
            }).PipeTo(self);
        }
    }
}
