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
        List<int> condsIndex = new List<int>();

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
                Table table = (Table)db[pTable];
                string[]  OutPut = new string[cols.Length];

                if (conds is null)
                {
                    string sk = "";
                    int skIndex = 0;
                    bool ctrl = true;

                    foreach (Column s in cols)
                    {
                        skIndex = 0;

                        if (table.getTable().ContainsKey(s.name))
                        {
                            if (ctrl)
                            {
                                sk = s.name + "\n";
                                ctrl = false;
                                foreach (string t in table.getTable()[s.name])
                                {
                                    sk += table.getTable()[s.name][skIndex];
                                    OutPut[skIndex] = sk;
                                    skIndex++;
                                }
                            }
                            else
                            {
                                foreach (string t in table.getTable()[s.name])
                                {
                                    OutPut[skIndex] += " " + table.getTable()[s.name][skIndex];
                                    skIndex++;
                                }
                            }
                        }
                        else
                        {
                            return Messages.ColumnDoesNotExist + " " + s.name;
                        }
                    }
                }
                else
                {
                    prepareConditions(table,conds);
                    foreach (Column s in cols)
                    {
                        if (table.getTable().ContainsKey(s.name))   //pendiente
                        {
                            
                        }
                        else
                        {
                            return Messages.ColumnDoesNotExist + " " + s.name;
                        }
                    }
                }
            }
            else
            {
                return Messages.TableDoesNotExist;
            }
            return null;

        } 
        
        private void prepareConditions(Table table ,string[] conds)
        {
            for (int condsCols = 0; condsCols + 1 < conds.Length; condsCols += 2)
            {
                string s = conds[condsCols];

                if (table.getTable().ContainsKey(s))
                {
                    if (table.getTable()[s].Contains(conds[condsCols + 1]))
                    {
                        for (int i = 0; i < table.getTable()[s].Count(); i++)   //sacar todas las coincidencias con la condicion dada REVISAR
                        {
                            if (table.getTable()[s][i].Equals(conds[condsCols + 1]))
                            {
                                condsIndex.Add(i);
                            }
                        }
                    }                   
                }
                else
                {
                     Console.WriteLine(Messages.ColumnDoesNotExist + " " + s);
                }
            }
        }
    }
}
