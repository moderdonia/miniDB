using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    public class DB
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
            SQLParser sqlparser = new SQLParser();
            SQLtype sqltype = sqlparser.Parser(query);
            return sqltype.Execute(this);
        }
    }
}
