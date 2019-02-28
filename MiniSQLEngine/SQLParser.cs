using MiniSQLEngine.QuerySystem.QueryTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    public class SQLParser
    {

        public SQLtype Parser(string query)
        {
            //Con *:
            string select1 = @"SELECT\s+(\*)\s+FROM\s+(\w+);";
            string select2 = @"SELECT\s+(\*)\s+FROM\s+(\w+)\s+WHERE\s+(\w+)\s*(=|<|>)\s*(\w+);";
            //Con una tabla:
            string select3 = @"SELECT\s+(\w+)\s+FROM\s+(\w+);";
            string select4 = @"SELECT\s+(\w+)\s+FROM\s+(\w+)\s+WHERE\s+(\w+)\s*(=|<|>)\s*(\w+);";
            //Con mas de una tabla:
            string select5 = @"SELECT\s+(\w+)(\,\s+(\w+))+\s+FROM\s+(\w+);";
            string select6 = @"SELECT\s+(\w+)(\,\s+(\w+))+\s+FROM\s+(\w+)\s+WHERE\s+(\w+)\s*(=|<|>)\s*(\w+);";

            //Insert
            string insert1 = @"INSERT\s+INTO\s+(\w+)\s+VALUES\((\w+)\);"; //CON TODOS SUS VALUES(1)
            string insert2 = @"INSERT\s + INTO\s + (\w +)\s + VALUES\s +\(*(\w +)(\,\s + (\w +))+\);"; //(CON TODOS SUS VALUES(+1))
            string insert3 = @"INSERT\s + INTO\s + (\w +)\s +\((\w +)\)\s + VALUES\s +\((\w +)\);"; //(CON UN VALUE)
            string insert4 = @"INSERT\s + INTO\s + (\w +)\s +\((\w +)(\,\s + (\w +))+\)\s + VALUES\s +\((\w +)(\,\s + (\w +))+\);"; //(completa)

            //Delete
            string delete = @"DELETE\s+FROM\s+(\w+)\s+WHERE\s+\w+\s*(=|<|>)\s*\w+;";

            //Update
            string update1 = @"UPDATE\s+(\w+)\s+SET\s+(\w+)\s*\=\s*(\w+)\s+WHERE\s+(\w+)\s*(=|<|>)\s*(\w+);";
            string update2 = @"UPDATE\s+(\w+)\s+SET\s+(\w+)\s*\=\s*(\w+)(\,\s+(\w+)\s*\=\s*(\w+)\s+)WHERE\s+(\w+)\s*(=|<|>)\s*(\w+);";

            //Drop Table
            string dropTable = @"DROP\s+TABLE\s+(\w+);";

            //Drop DB
            string dropDB = @"DROP\s+DATABASE\s+(\w+);";

            //Create DB
            string createDB = @"CREATE\s+DATABASE\s+(\w+);";

            //Backup DB
            string backupDB = @"BACKUP\s+DATABASE\s+(\w+)\s+TO\s+DISK\s*\=\s*\'([^\']+)\';";

            //Create Table
            string createTable = @"(CREATE\s+TABLE)\s+(\w+)\s+\((\w+\s+(INT|DOUBLE|TEXT)(\s+|\,\s+\w+\s+(INT|DOUBLE|TEXT))+)\,\s+(PRIMARY\s+KEY)\s+\((\w+)\)\,\s+(FOREIGN\s+KEY)\s+\((\w+)\)\s+REFERENCES\s+(\w+)\s+\((\w+)\);";

            string[] camp1 = new string[10];
            string camp2 = "";
            string[] camp3 = new string[10];
            string campo1 = "";

            //SELECT
            Match matchS1 = Regex.Match(query, select1);
            Match matchS2 = Regex.Match(query, select2);
            Match matchS3 = Regex.Match(query, select3);
            Match matchS4 = Regex.Match(query, select4);
            Match matchS5 = Regex.Match(query, select5);
            Match matchS6 = Regex.Match(query, select6);

            if (Regex.Match(query, select1).Success)
            {
                camp1[0] = matchS1.Groups[1].Value;
                camp2 = matchS1.Groups[2].Value;

                SQLtype sentencia = new Select(camp2,camp1,camp3);

                return sentencia;
            }
            else if (Regex.Match(query, select2).Success)
            {
                camp1[0] = matchS2.Groups[1].Value;
                camp2 = matchS2.Groups[2].Value;
                for(int i = 0; i < matchS2.Groups[3].Length; i++)
                { 
                    camp3[i] = matchS2.Groups[3].Captures[i].Value;
                    
                }
                
                SQLtype sentencia = new Select(camp2, camp1, camp3);

                return sentencia;
            }
            else if (Regex.Match(query, select3).Success)
            {
                camp1[0] = matchS3.Groups[1].Value;
                camp2 = matchS3.Groups[2].Value;
                
                SQLtype sentencia = new Select(camp2, camp1, camp3);

                return sentencia;
            }
            else if (Regex.Match(query, select4).Success)
            {
                camp1[0] = matchS4.Groups[1].Value;
                camp2 = matchS4.Groups[2].Value;
                camp3[0] = matchS4.Groups[3].Value;
                camp3[1] = matchS4.Groups[4].Value;
                camp3[2] = matchS4.Groups[5].Value;

                SQLtype sentencia = new Select(camp2, camp1, camp3);

                return sentencia;
            }
            else if (Regex.Match(query, select5).Success)
            {
                Console.WriteLine(matchS5.Groups[2].Captures[1].Value);
                camp1[0] = matchS5.Groups[1].Value;
                camp1[1] = matchS5.Groups[2].Value;
                camp2 = matchS5.Groups[4].Value;
                
                
                SQLtype sentencia = new Select(camp2, camp1, camp3);

                return sentencia;
            }
            else if (Regex.Match(query, select6).Success)
            {

                camp1[0] = matchS6.Groups[0].Value;
                camp1[1] = matchS6.Groups[2].Value;
                camp2 = matchS6.Groups[3].Value;
                camp3[0] = matchS6.Groups[4].Value;
                camp3[1] = matchS6.Groups[5].Value;
                camp3[2] = matchS6.Groups[6].Value;

                SQLtype sentencia = new Select(camp2, camp1, camp3);

                return sentencia;
            }


            //Drop table

            Match matchDT = Regex.Match(query, dropTable);

            if (matchDT.Success)
            {
                campo1 = matchDT.Groups[0].Value;

                SQLtype sentencia = new DropTable(campo1);

                return sentencia;
            }


            string cinsert1;
            string[] cinsert2 = null;
            string[] cinsert3 = null;

            //Insert 

            Match matchI1 = Regex.Match(query, insert1);
            Match matchI2 = Regex.Match(query, insert2);
            Match matchI3 = Regex.Match(query, insert3);
            Match matchI4 = Regex.Match(query, insert4);

            if (Regex.Match(query, insert1).Success)
            {
                cinsert1 = matchI1.Groups[1].Value;
                cinsert3[0] = matchI1.Groups[2].Value;

                SQLtype sentencia = new Select(cinsert1, cinsert2, cinsert3);

                return sentencia;
            }
           




            Match match3 = Regex.Match(query, delete);
            //Match match4 = Regex.Match(query, update);
            Match match5 = Regex.Match(query, dropTable);
            Match match6 = Regex.Match(query, dropDB);
            Match match7 = Regex.Match(query, createDB);
            Match match8 = Regex.Match(query, backupDB);
            Match match9 = Regex.Match(query, createTable);


            return null;

        }
       
    }
}
