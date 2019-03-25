using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine.QuerySystem
{
    public class Column
    {
        public string name { get; set; }
        public string type { get; set; }

        public Column(string name, string type)
        {
            this.name = name;
            this.type = type;
        }

    }
}
