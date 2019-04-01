using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine.QuerySystem
{
    public class Table
    {
        string name;
        Dictionary<string, List<string>> dc = new Dictionary<string, List<string>>();

        public Table(string name, List<Column> cols)
        {
            this.name = name;
            foreach (Column s in cols)
            {
                if(!(s.name is null))
                {
                    List<string> list = new List<string>();
                    if (!dc.ContainsKey(s.name))
                    {
                        dc.Add(s.name, list);
                    }
                    else
                    {
                        Console.WriteLine(Messages.TableExist);
                    }
                    
                }          
            } 
            
        }
        public string getName()
        {
            return name;
        }
        
        public Dictionary<string, List<string>> getTable()
        {
            return dc;
        }

        /*
        public Boolean compareQuerys(int index, )
        {

        }
        */
    }
}
