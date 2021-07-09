using System;
using Akka.Actor;
using Akka.Routing;

namespace AtLeastOnce
{
    class Program
    {
        static void Main(string[] args)
        {
            var sys = ActorSystem.Create("sys");
            var database = new DatabaseService();

            var writer = sys.ActorOf(Props.Create(() => new WriterActor(database)).WithRouter(new RoundRobinPool(10)));
            var main = sys.ActorOf(Props.Create(() => new StoreActor(writer)));

            for (int i = 0; i < 1000; i++)
            {
                main.Tell(new Insert(i, $"Hi {i}"));
            }

            Console.ReadKey();
        }
    }
}
