using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Akka.Actor;

namespace PriorityMailbox
{
    public class WorkerActor : ReceiveActor
    {
        public WorkerActor()
        {
            Receive<WorkMessage>(m =>
            {
                Console.WriteLine("Working");
                Thread.Sleep(1000);
            });
            Receive<PanicMessage>(m => Console.WriteLine("Panic"));
        }
    }
}
