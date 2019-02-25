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

            string insert = @"(INSERT)\s+(INTO)\s+(\w+)(\s+|\s+\((\w+)\)\s+|\s+\((\w+)(\,\s+(\w+))+\))\s+(VALUES)(\s+\((\w+)\)|\s+\((\w+)(\,\s+(\w+))+\));";
            string delete = @"(DELETE)\s + (FROM)\s + (\w +)\s + (WHERE)\s +\w +\s * (=|<|>)\s *\w +;";
            string update = @"(UPDATE)\s+(\w+)\s+(SET)\s+(\w+)\s*\=\s*(\w+)(\s+|\,\s+(\w+)\s*\=\s*(\w+)\s+)(WHERE)\s+(\w+)\s*(=|<|>)\s*(\w+);";

            string dropTable = @"(DROP)\s+(TABLE)\s+(\w+);";
            string dropDB = @"(DROP)\s+(DATABASE)\s+(\w+);";
            string createDB = @"(CREATE)\s+(DATABASE)\s+(\w+);";
            string backupDB = @"(BACKUP\s+DATABASE)\s+(\w+)\s+TO\s+DISK\s*\=\s*\'([^\']+)\';";
            string createTable = @"(CREATE\s+TABLE)\s+(\w+)\s+\((\w+\s+(INT|DOUBLE|TEXT)(\s+|\,\s+\w+\s+(INT|DOUBLE|TEXT))+)\,\s+(PRIMARY\s+KEY)\s+\((\w+)\)\,\s+(FOREIGN\s+KEY)\s+\((\w+)\)\s+REFERENCES\s+(\w+)\s+\((\w+)\);";

            string[] camp1 = null;
            string camp2;
            string[] camp3 = null;
            string campo1;

            //SELECT
            Match matchS1 = Regex.Match(query, select1);
            Match matchS2 = Regex.Match(query, select2);
            Match matchS3 = Regex.Match(query, select3);
            Match matchS4 = Regex.Match(query, select4);
            Match matchS5 = Regex.Match(query, select5);
            Match matchS6 = Regex.Match(query, select6);

            if (Regex.Match(query, select1).Success)
            {
                camp1[0] = matchS1.Groups[0].Value;
                camp2 = matchS1.Groups[1].Value;

                SQLtype sentencia = new Select(camp2,camp1,camp3);

                return sentencia;
            }
            else if (Regex.Match(query, select2).Success)
            {
                camp1[0] = matchS2.Groups[0].Value;
                camp2 = matchS2.Groups[1].Value;
                camp3[0] = matchS2.Groups[2].Value;
                camp3[1] = matchS2.Groups[2].Value;
                camp3[2] = matchS2.Groups[2].Value;

                SQLtype sentencia = new Select(camp2, camp1, camp3);

                return sentencia;
            }
            else if (Regex.Match(query, select3).Success)
            {
                camp1[0] = matchS3.Groups[0].Value;
                camp2 = matchS3.Groups[1].Value;
                
                SQLtype sentencia = new Select(camp2, camp1, camp3);

                return sentencia;
            }
            else if (Regex.Match(query, select4).Success)
            {
                camp1[0] = matchS4.Groups[0].Value;
                camp2 = matchS4.Groups[1].Value;
                camp3[0] = matchS4.Groups[2].Value;
                camp3[1] = matchS4.Groups[3].Value;
                camp3[2] = matchS4.Groups[4].Value;

                SQLtype sentencia = new Select(camp2, camp1, camp3);

                return sentencia;
            }
            else if (Regex.Match(query, select5).Success)
            {
                camp1[0] = matchS5.Groups[0].Value;
                camp1[1] = matchS5.Groups[2].Value;
                camp2 = matchS5.Groups[3].Value;
                
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


            Match matchDT = Regex.Match(query, dropTable);

            if (matchDT.Success)
            {
                campo1 = matchDT.Groups[0].Value;

                SQLtype sentencia = new DropTable(campo1);

                return sentencia;
            }

            Match match3 = Regex.Match(query, delete);
            Match match4 = Regex.Match(query, update);
            Match match5 = Regex.Match(query, dropTable);
            Match match6 = Regex.Match(query, dropDB);
            Match match7 = Regex.Match(query, createDB);
            Match match8 = Regex.Match(query, backupDB);
            Match match9 = Regex.Match(query, createTable);




        }
       
    }
}
