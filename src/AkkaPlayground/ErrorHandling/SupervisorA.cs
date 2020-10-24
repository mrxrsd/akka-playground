using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;

namespace ErrorHandling
{
    public class SupervisorA : ReceiveActor
    {
        public SupervisorA()
        {
            Context.ActorOf(Props.Create(()=> new Child(new ExternalService())), "1");
            Context.ActorOf(Props.Create(() => new Child(new ExternalService())), "2");

            Receive<Command>(p => Context.Child(p.Destiny).Forward(p));
            
        }


        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(
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
