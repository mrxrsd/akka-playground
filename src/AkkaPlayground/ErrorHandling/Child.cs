using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;

namespace ErrorHandling
{
    public class Child : ReceiveActor
    {
        private readonly ExternalService _service;
        private int _count;

        public Child(ExternalService service)
        {
            _service = service;

            Receive<Command>(p =>
            {
                _count++;
                Console.WriteLine($"Message {_count} Received. Content: {p.Msg}");

                if (p.Msg == "resume")
                {
                    throw new ArithmeticException();
                }

                if (p.Msg == "restart")
                {
                    throw new NullReferenceException();
                }

                if (p.Msg == "getdata")
                {
                    service.GetGoodData().PipeTo(Context.Self);
                }

                if (p.Msg == "geterror")
                {
                    service.GetErrorData().PipeTo(Context.Self);
                }

            });


            Receive<string>(s =>
            {
                Console.WriteLine($"Data: {s}");
            });
        }


        protected override void PreRestart(Exception reason, object message)
        {
            Console.WriteLine($"Restarting {Context.Self.Path}...");
            base.PreRestart(reason, message);
        }
    }
}
