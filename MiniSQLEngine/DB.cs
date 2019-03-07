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
        //private Dictionary<string,Table> db;
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
            List<Column> listcol = attbs.OfType<Column>().ToList();
            
            Table table = new Table(name,listcol);   
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
                return Messages.InsertSuccess;
            }
            else
            {
                return Messages.TableDoesNotExist;
            }
        }
        public string deleteTable(string table)
        {
            if (db.ContainsKey(table))
            {
                db.Remove(table);
                return Messages.DeleteTableSuccess;

            }
            else
            {
                return Messages.TableDoesNotExist;
            }
            
        }
        public string exeSelect(string pTable, Column[] cols,string[] conds)
        {
            if (db.ContainsKey(pTable))
            {
                Table table = (Table) db[pTable];
                foreach (Column s in cols)
                {
                    
                }
            }
            else
            {
                return Messages.TableDoesNotExist;
            }

                return null;
        }
        
        
    }
}
