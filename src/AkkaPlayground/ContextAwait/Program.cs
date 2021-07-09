using System;
using Akka.Actor;

namespace ContextAwait
{
    class Program
    {
        static void Main(string[] args)
        {
            var sys = ActorSystem.Create("sys");
            var main = sys.ActorOf(Props.Create<MainActor>(), "a");


            main.Tell(new Hi());
            main.Tell(new Hi());
            main.Tell(new Hi());
            main.Tell(new Hi());
            main.Tell(new Hi());

            Console.ReadKey();
        }
    }
}
