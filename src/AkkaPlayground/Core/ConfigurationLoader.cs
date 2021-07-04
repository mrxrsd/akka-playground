using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Akka.Configuration;

namespace Core
{
    public static class ConfigurationLoader
    {
        public static Config Load(Dictionary<string,string> vals = null) => LoadConfig("akka.conf", vals);

        public static Config LoadConfig(string configFile, Dictionary<string,string> vals)
        {
            if (File.Exists(configFile))
            {
                string config = File.ReadAllText(configFile);
                if (vals != null && vals.Any())
                {
                    config += "\n";
                    foreach (var val in vals)
                    {
                        config += $"{val.Key}=\"{val.Value}\"\n";
                    }
            
                }

                return ConfigurationFactory.ParseString(config);
            }

            return Config.Empty;
        }
    }
}
