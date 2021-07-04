using System;
using System.Collections.Generic;
using Core;

namespace HoconSubstitution
{
    class Program
    {
        static void Main(string[] args)
        {
            var dic = new Dictionary<string, string>
            {
                {"akka.persistence.journal.redis.connection-string", "injected in runtime"}

            };

            var config = ConfigurationLoader.LoadConfig("akka.conf", dic);
            Console.WriteLine($"{config.GetValue("akka.persistence.journal.redis.connection-string")}");
                
            Console.ReadKey();
        }
    }
}
