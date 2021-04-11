using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;

namespace Initialization
{
    public class ChildB : ReceiveActor
    {
        private Foo _foo;

        public ChildB(Func<Foo> foo)
        {
            _foo = foo();

            Receive<Print>(p =>
            {
                Console.WriteLine($"Print ChildB: {_foo.Value}");
                Context.Sender.Tell(new Ack());
            });

            Receive<ToggleValue>(p =>
            {
                _foo = new Foo("b");
                Context.Sender.Tell(new Ack());
            });

            Receive<Kill>(p =>
            {
               throw new ArgumentException();

            });
        }


        protected override void PreRestart(Exception reason, object message)
        {
            Console.WriteLine($"Restarting {Context.Self.Path}...");
            base.PreRestart(reason, message);
        }
    }
}
