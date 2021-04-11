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

            //Console.WriteLine("SupervisorA - OneForAllStrategy");
            //Console.WriteLine("Input a text and press [Enter].");
            //Console.WriteLine("Special 'Commands': restart, resume, getdata, geterror.");
            //Console.WriteLine(" ");

            //while (true)
            //{
            //    var val = Console.ReadLine();
            //    if (string.IsNullOrEmpty(val)) break;

            //    supervisorA.Tell(new Dns.Command("1", val));
            //}


            //Console.WriteLine("SupervisorB - AllForOneStrategy");
            //Console.WriteLine("Sending command 'restart' to SupervisorB. All children will be restarted.");
            //supervisorB.Tell(new Dns.Command("1", "restart"));
            Console.ReadKey();
        }
    }
}
