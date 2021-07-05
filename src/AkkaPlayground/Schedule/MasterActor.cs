using System;
using Akka.Actor;
using Core.Messages.Common;

namespace Schedule
{
    public class MasterActor : ReceiveActor, IWithTimers
    {

        public MasterActor()
        {

            Receive<WakeUp>(up =>
            { 
                Console.WriteLine("Hi!");
            });
        }

        protected override void PreStart()
        {
            Timers.StartPeriodicTimer("wake-up",new WakeUp(""), TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2));
        }

        public ITimerScheduler Timers { get; set; }
    }
}