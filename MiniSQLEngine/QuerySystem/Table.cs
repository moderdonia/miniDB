using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine.QuerySystem
{
    class Table
    {
        SQLtype sql;
        string name;
        Dictionary<string,List<string>> dc;

        public Table(string name, List<string> list)
        {
            this.name = name;
            foreach (string s in list)
            {
                dc.Add(s, new List<string>());
                
            } 
            
        }
    }
}
