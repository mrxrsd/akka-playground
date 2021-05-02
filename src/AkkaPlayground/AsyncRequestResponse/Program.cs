using System;
using System.Threading.Tasks;
using Akka.Actor;

namespace AsyncRequestResponse
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var sys = ActorSystem.Create("sys");
            var master = sys.ActorOf(Props.Create<OrderCoordinatorActor>(), "master");

            Console.WriteLine("asking cheese pizza");
            var answer = await master.Ask(new AskPizza("cheese"));
            Console.WriteLine(answer);

            Console.WriteLine("asking pepperoni pizza");
            var answer2 = await master.Ask(new AskPizza("pepperoni"));
            Console.WriteLine(answer2);

            Console.ReadKey();


        }
    }
}
