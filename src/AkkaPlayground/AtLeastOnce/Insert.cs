using System;
using System.Collections.Generic;
using System.Text;

namespace AtLeastOnce
{
    public class Insert
    {
        public int Id { get; }
        public string Msg { get; }

        public Insert(int id, string msg)
        {
            Id = id;
            Msg = msg;
        }
    }
}
