using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;

namespace Initialization
{
    public class Master : ReceiveActor
    {
        private IActorRef _childA;
        private IActorRef _childB;
        private Foo _foo;

        public Master()
        {
            _foo = new Foo("a");

            Receive<ToggleValue>(f =>
            {
                _foo = new Foo("master");
                _childA.Forward(f);
                _childB.Forward(f);
            });

            Receive<Kill>(f =>
            {
                _childA.Forward(f);
                _childB.Forward(f);
            });

            Receive<Print>(p =>
            {
                Console.WriteLine($"Print Master: {_foo.Value}");
                _childA.Forward(p);
                _childB.Forward(p);
            });

            _childA = Context.ActorOf(Props.Create(() => new ChildA(_foo)),"A");
            _childB = Context.ActorOf(Props.Create(() => new ChildB(()=> _foo)),"B");
        }



    }
}
