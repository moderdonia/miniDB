﻿using MiniSQLEngine.QuerySystem;
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
        private Dictionary<string,Table> db;
        //private Hashtable db;
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
                return "Some issues happens";
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
        public string insertData(string pTable,string[]cols, string[] data) //name = table , data = values // Cambiar metodo, tiene que recibir tambien las columnas sobre las que insertar
        {
            prepareColumns(data);
            if (db.ContainsKey(pTable))
            {
               foreach(Column d in listColAux)
                {
                    if (!(d.name is null))
                    {
                        foreach (string s in data)
                        {
                            db[pTable].getTable()[s].Add(data[0]); // revisar [0]
                        }
                        return Messages.InsertSuccess;
                    }
                }
                return Messages.InsertSuccess;
            }
            else
            {
                return Messages.TableDoesNotExist;
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
                    foreach (Column s in listColAux) //se crean demasiados espacios nulos
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
                                return Messages.ColumnDoesNotExist + " " + s.name;
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
                Column c = new Column(s, "string");
                listColAux.Add(c);
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
