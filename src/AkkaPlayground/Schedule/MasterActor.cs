using System;
using Akka.Actor;
using Core.Messages.Common;

namespace Schedule
{
    public class MasterActor : ReceiveActor
    {

        public MasterActor()
        {
            Context.System.Scheduler.ScheduleTellRepeatedly(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2), Context.Self, new WakeUp(""), Self);

            Receive<WakeUp>(up =>
            { 
                Console.WriteLine("Hi!");
            });
        }
    }
}