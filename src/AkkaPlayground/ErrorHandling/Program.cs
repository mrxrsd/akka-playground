using System;
using Akka.Actor;

namespace ErrorHandling
{
    class Program
    {
        static void Main(string[] args)
        {
            var sys = ActorSystem.Create("sys");
            var supervisorA = sys.ActorOf(Props.Create<SupervisorA>(), "a");
            var supervisorB = sys.ActorOf(Props.Create<SupervisorB>(), "b");

            Console.WriteLine("SupervisorA - OneForAllStrategy");
            Console.WriteLine("Input a text and press [Enter].");
            Console.WriteLine("Special 'Commands': restart, resume, getdata, geterror.");
            Console.WriteLine(" ");

            while (true)
            {
                var val = Console.ReadLine();
                if (string.IsNullOrEmpty(val)) break;

                supervisorA.Tell(new Command("1", val));
            }


            Console.WriteLine("SupervisorB - AllForOneStrategy");
            Console.WriteLine("Sending command 'restart' to SupervisorB. All children will be restarted.");
            supervisorB.Tell(new Command("1", "restart"));
            Console.ReadKey();
        }
    }
}
