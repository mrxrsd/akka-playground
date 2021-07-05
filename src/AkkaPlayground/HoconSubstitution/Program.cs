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
                {"redis-connection-string-secrets", "injected in runtime"}

            };

            var config = ConfigurationLoader.LoadConfig("akka.conf", dic);
            Console.WriteLine($"akka.persistence.journal.redis.connection-string: {config.GetValue("akka.persistence.journal.redis.connection-string")}");
                
            Console.ReadKey();
        }
    }
}
