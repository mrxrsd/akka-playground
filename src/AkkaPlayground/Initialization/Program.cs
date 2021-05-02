using System;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.IO;

namespace Initialization
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var sys = ActorSystem.Create("sys");
            var master = sys.ActorOf(Props.Create<Master>(), "master");

            Console.WriteLine(">> Start ");
            await master.Ask(new Print());
            Console.WriteLine(" ");

            Console.WriteLine(">> Toggle ");
            await master.Ask(new ToggleValue());
            await master.Ask(new Print()); 
            Console.WriteLine(" ");

            Console.WriteLine(">> Kill ");
            master.Tell(new Kill());
            await master.Ask(new Print());

            Console.ReadKey();
        }
    }
}
