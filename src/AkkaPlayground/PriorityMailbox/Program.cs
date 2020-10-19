using System;
using System.Threading;
using Akka.Actor;
using Core;

namespace PriorityMailbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var sys = ActorSystem.Create("sys", ConfigurationLoader.Load());
            var actor = sys.ActorOf(Props.Create<WorkerActor>().WithMailbox("worker-priority-mailbox"));

            actor.Tell(new WorkMessage());
            actor.Tell(new WorkMessage());
            actor.Tell(new WorkMessage());
            actor.Tell(new WorkMessage());
            Thread.Sleep(1000);
            actor.Tell(new PanicMessage());


            Console.ReadKey();
        }
    }
}
