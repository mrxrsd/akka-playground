using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace AtLeastOnce
{
    public class DatabaseService
    {
        private Dictionary<int, string> _table = new Dictionary<int, string>();
        public async Task<bool> Write(int id, string msg)
        {
            try
            {
                var r = new Random();
                await Task.Delay(TimeSpan.FromMilliseconds(r.NextDouble() * 300));
                _table.Add(id, msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR!!!!");
            }

            return true;
        }
    }
}
