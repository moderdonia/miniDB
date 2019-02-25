using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    class DB
    {
        Hashtable ht;

        string name;

        public DB(string name)
        {
            ht = new Hashtable();

            this.name = name;
        }

        public string runQuery(string query)
        {
            return null;
        }
    }
}
