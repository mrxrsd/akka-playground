using System;
using Akka.Actor;

namespace Schedule
{
    class Program
    {
        static void Main(string[] args)
        {
            var sys = ActorSystem.Create("sys");
            var master = sys.ActorOf(Props.Create<MasterActor>(), "master");


            Console.ReadKey();
        }
    }
}
