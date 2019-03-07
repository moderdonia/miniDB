using MiniSQLEngine.QuerySystem;
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
        //private Dictionary<string,string> db;
        private Hashtable db;
        string name;

        public string runQuery(string query)
        {
            SQLParser sqlparser = new SQLParser();
            SQLtype sqltype = sqlparser.Parser(query);
            return sqltype.Execute(this);
        }

        public DB(string name)
        {
            db = new Hashtable();

            this.name = name;
        }

        public string addtable(string name, Column[] attbs)
        {
            Hashtable table = new Hashtable();
            foreach (Column s in attbs)
            {

                List<string> list = new List<string>();
                table.Add(s,list);

            }
                
            db.Add(name, table);
            return Messages.CreateTableSuccess;
        }
        public string insertData(string name, string[] data)
        {
            if (db.ContainsKey(name))
            {
                int i = 0;
                foreach(string s in ((Hashtable) db[name]).Keys)
                {
                    ((List<string>)((Hashtable)db[name])[s]).Add(data[i]);
                    i++;   
                }
            }
            else
            {
                return "cannot insert data";
            }
            return "data inserted correctly";
        }
        
        
    }
}
