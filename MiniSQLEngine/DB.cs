using MiniSQLEngine.QuerySystem;
using System;
using System.Collections.Generic;
using System.Linq;


namespace MiniSQLEngine
{
    public class DB
    {
        private Dictionary<string,Table> db;
        Boolean ctrl;
        string name;
        List<int> condsIndex = new List<int>();
        List<Column> listColAux = new List<Column>();
        

        public string runQuery(string query)
        {
            SQLParser sqlparser = new SQLParser();
            SQLtype sqltype = sqlparser.Parser(query);
            if(!(sqltype is null))
            {
                return sqltype.Execute(this);
            }
            else
            {
                return "Query unrecognized";
            }

        }

        public DB(string name)
        {
            db = new Dictionary<string, Table>();
            this.name = name;
        }

        public string createTable(string name, string[] attbs)
        {
            prepareColumns(attbs);
            Table table = new Table(name,listColAux);
            if (!db.ContainsKey(name))
            {
                db.Add(name, table);
                return Messages.CreateTableSuccess;
            }
            else
            {
                return Messages.DatabaseExist;
            }
            
        }

        public string insertData(string pTable, string[]cols, string[] data) //name = table , data = values , cols = attb
        {
            int i =0;
            int x = data.Length;
            ctrl = true;
            List<string> ordenAux = new List<string>();
            IEnumerable<string> missCols;
            prepareColumns(cols);

            if (listColAux.Count == 0)
            {
                if (db.ContainsKey(pTable))
                {
                    foreach (string d in db[pTable].getTable().Keys)
                    {

                        if (!(d is null))
                        {
                            ctrl = true;
                            while (ctrl && i < data.Length)
                            {
                                if (data[i] != null && db[pTable].getTable().ContainsKey(d))
                                {
                                    string aux = data[i].ToString();
                                    db[pTable].getTable()[d].Add(aux);                                   
                                }
                                i++;
                                ctrl = false;
                            }
                        }
                    }
                    return Messages.InsertSuccess;
                }
                else
                {
                    return Messages.TableDoesNotExist;
                }
            }
            else
            { 
                foreach (Column k in listColAux)
                {
                    ordenAux.Add(k.name);    
                }

                missCols = db[pTable].getTable().Keys.Except(ordenAux);
                IEnumerator<string> it = missCols.GetEnumerator();

                if (db.ContainsKey(pTable))
                {
                    foreach (Column d in listColAux)
                    {
                        if (!(d is null))
                        {
                            ctrl = true;
                            while (ctrl)
                            {
                                if (data[i] != null && db[pTable].getTable().ContainsKey(d.name))
                                {
                                    string aux = data[i].ToString();
                                    db[pTable].getTable()[d.name].Add(aux);
                                }
                                i++;
                                ctrl = false;
                            }
                        }
                    }
                    if (missCols.Count() != 0)
                    {
                        while (it.MoveNext())
                        {
                            db[pTable].getTable()[it.Current].Add("null");
                        }
                    }
                    return Messages.InsertSuccess;
                }
                else
                {
                    return Messages.TableDoesNotExist;
                }
            }        
        }

        public string dropTable(string table)
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

        public string deleteTuple(string pTable, string[] conds)
        {
            Table table = db[pTable];

            prepareConditions(table, conds);
            for(int i=0; i < condsIndex.Count ; i=+2)
            {
                if (table.getTable().ContainsKey(conds[i]))
                {
                    foreach (int x in condsIndex)
                    {
                        foreach(List<string> h in table.getTable().Values)
                        {
                            h.RemoveAt(x);
                        }
                        
                    }   
                }
                else
                {
                    return Messages.TableDoesNotExist;
                }         
            }
            return Messages.TupleDeleteSuccess;
        }

        public string exeUpdate(string pTable, string[] cols,string[] conds)
        {
            prepareColumns(cols);

            if (db.ContainsKey(pTable))
            {
                Table table = db[pTable];

                if (conds[0] == null)
                {
                    foreach (Column s in listColAux)
                    {
                        if (!(s.name is null))
                        {
                            if (table.getTable().ContainsKey(s.name))
                            {
                                for (int t = 0; t < table.getTable().Count;t++)
                                {
                                    for (int g = 0; g < cols.Length; g += 2)
                                    {
                                        table.getTable()[s.name][t] = cols[g];
                                    }
                                }
                                return Messages.TupleUpdateSuccess;
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
                    prepareConditions(table, conds);
                    foreach (Column s in listColAux) 
                    {
                        if (!(s.name is null))
                        {
                            if (table.getTable().ContainsKey(s.name))   
                            {
                                foreach (int i in condsIndex)
                                {
                                    for(int g = 0; g < cols.Length; g += 2)
                                    {
                                        if(cols[g+1] != null)
                                        {
                                            table.getTable()[s.name][i] = cols[g + 2];
                                        }
                                        
                                    }
                                    
                                }
                                return Messages.TupleUpdateSuccess;
                            }
                            else
                            {
                                return Messages.ColumnDoesNotExist;
                            }
                        }
                    }
                }
                return Messages.InsertSuccess;
            }
            else
            {
                return Messages.TableDoesNotExist;
            }
        }



        public string exeSelect(string pTable, string[] cols,string[] conds)
        {
            prepareColumns(cols);
            if (db.ContainsKey(pTable))
            {
                Table table = db[pTable];
                
                //string[]  OutPut = new string[cols.Length];
                string sk = "";
                //int skIndex = 0;

                if (conds[0]==null)
                { 
                    foreach (Column s in listColAux)
                    {
                        if (!(s.name is null))
                        {
                            //skIndex = 0;
                            sk += "\n" + s.name + " : ";

                            if (table.getTable().ContainsKey(s.name))
                            {   
                                foreach (string t in table.getTable()[s.name])
                                {
                                    sk += "| " + t + " |";
                                    //skIndex++;
                                }
                                //if (ctrl)
                                //{
                                //sk = s.name + "\n";
                                //ctrl = false;
                                //}
                                //else
                                //{
                                //    foreach (string t in table.getTable()[s.name])
                                //    {
                                //       OutPut[skIndex] += " " + table.getTable()[s.name][skIndex];
                                //        skIndex++;
                                //    }
                                // }
                            }
                            else
                            {
                                return Messages.ColumnDoesNotExist + " " + s.name; //saca error aun quitando correctamente los datos
                            }
                        }
                    }
                }
                else
                {
                    prepareConditions(table, conds);
                    foreach (Column s in listColAux) //se crean demasiados espacios nulos
                    {   
                        if(!(s.name is null))
                        {
                            sk += "\n" + s.name + " : ";
                            //skIndex = 0;

                            if (table.getTable().ContainsKey(s.name))   //pendiente
                            {
                                foreach (int i in condsIndex)
                                {
                                    sk += "| " + table.getTable()[s.name][i] + " |";
                                    //skIndex++;
                                }
                            }
                            else
                            {
                                return Messages.ColumnDoesNotExist + " " + s.name;
                            }
                            break;
                            
                        }    
                    }
                }
                sk += "\n";
                return sk;
            }
            else
            {
                return Messages.TableDoesNotExist;
            }
        } 
        

        //Internal Methods

        private void prepareColumns(string[] cols)
        {
            condsIndex.Clear();
            listColAux.Clear();
            foreach (string s in cols)
            {
                if(s != null)
                {
                    Column c = new Column(s, "string");
                    listColAux.Add(c);
                }
                
            }
        }

        private void prepareConditions(Table table ,string[] conds)     //saca error aun quitando correctamente los datos
        {
            condsIndex.Clear();           
            for (int condsCols = 0; condsCols < conds.Length; condsCols += 1)
            {
                if(conds[condsCols]!=null)
                {
                    string s = conds[condsCols];

                    switch (s)
                    {
                        case "<":
                            if (table.getTable().ContainsKey(conds[condsCols-1]))
                            {
                                for (int i = 0; i < table.getTable()[conds[condsCols - 1]].Count(); i++)   //REVISAR
                                {
                                    if (Int32.Parse(table.getTable()[conds[condsCols - 1]][i]) < Int32.Parse(conds[condsCols+1]))
                                    {
                                        condsIndex.Add(i);
                                    }
                                }
                            }
                            break;

                        case "<=":
                            if (table.getTable().ContainsKey(conds[condsCols - 1]))
                            {
                                for (int i = 0; i < table.getTable()[conds[condsCols - 1]].Count(); i++)   //REVISAR
                                {
                                    if (Int32.Parse(table.getTable()[conds[condsCols - 1]][i]) <= Int32.Parse(conds[condsCols + 1]))
                                    {
                                        condsIndex.Add(i);
                                    }
                                }
                            }
                            break;

                        case ">":
                            if (table.getTable().ContainsKey(conds[condsCols - 1]))
                            {
                                for (int i = 0; i < table.getTable()[conds[condsCols - 1]].Count(); i++)   //REVISAR
                                {
                                    if (Int32.Parse(table.getTable()[conds[condsCols - 1]][i]) > Int32.Parse(conds[condsCols + 1]))
                                    {
                                        condsIndex.Add(i);
                                    }
                                }
                            }
                            break;

                        case ">=":
                            if (table.getTable().ContainsKey(conds[condsCols - 1]))
                            {
                                for (int i = 0; i < table.getTable()[conds[condsCols - 1]].Count(); i++)   //REVISAR
                                {
                                    if (Int32.Parse(table.getTable()[conds[condsCols - 1]][i]) >= Int32.Parse(conds[condsCols + 1]))
                                    {
                                        condsIndex.Add(i);
                                    }
                                }
                            }
                            break;

                        default:
                            try
                            {
                                if (table.getTable().ContainsKey(s))
                                {
                                    for (int i = 0; i < table.getTable()[s].Count(); i++)   //REVISAR
                                    {
                                        if (table.getTable()[s][i].Equals(conds[condsCols+2]))
                                        {
                                            condsIndex.Add(i);
                                        }
                                    }
                                }
                                break;
                            }
                            catch
                            {
                                break;
                            }
                    }                     
                }               
            }       
        }
    }
}
