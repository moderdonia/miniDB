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
        string name;
        Dictionary<string,List<Column>> dc;

        public Table(string name, List<Column> cols)
        {
            this.name = name;
            foreach (Column s in cols)
            {
               // List<s.type> list = new List<>();
              //  dc.Add(s.name, list);
                
            } 
            
        }
    }
}
