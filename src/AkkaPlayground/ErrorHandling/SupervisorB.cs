using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;

namespace ErrorHandling
{
    public class SupervisorB : ReceiveActor
    {
        public SupervisorB()
        {
            Context.ActorOf(Props.Create(() => new Child(new ExternalService())), "1");
            Context.ActorOf(Props.Create(() => new Child(new ExternalService())), "2");

            Receive<Command>(p => Context.Child(p.Destiny).Forward(p));
            
        }


        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new AllForOneStrategy(
                maxNrOfRetries: 10,
                withinTimeRange: TimeSpan.FromMinutes(1),
                localOnlyDecider: ex =>
                {
                    switch (ex)
                    {
                        case ArithmeticException ae:
                            return Directive.Resume;
                        case NullReferenceException nre:
                            return Directive.Restart;
                        case ArgumentException are:
                            return Directive.Restart;
                        default:
                            return Directive.Escalate;
                    }
                });
        }
    }
}
