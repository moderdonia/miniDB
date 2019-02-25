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

        public SQLtype parser(string query)
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
            //SELECT
            Match matchS1 = Regex.Match(query, select1);

            if (Regex.Match(query, select1).Success)
            {
                camp1[0] = matchS1.Groups[0].Value;
                camp2 = matchS1.Groups[1].Value;

                SQLtype sentencia = new Select(camp2,camp1,camp3);

                return sentencia;
            }
            else if (Regex.Match(query, select2).Success)
            {
            }

            Match matchS2 = ;
            Match matchS3 = Regex.Match(query, select3);
            Match matchS4 = Regex.Match(query, select4);
            Match matchS5 = Regex.Match(query, select5);
            Match matchS6 = Regex.Match(query, select6);


            
            else if (matchS2.Success)
            {

            }
            else if (matchS3.Success)
            {

            }
            else if (matchS4.Success)
            {

            }
            else if (matchS5.Success)
            {

            }
            else if (matchS6.Success)
            {

            }



            Match match2 = Regex.Match(query, insert);
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
