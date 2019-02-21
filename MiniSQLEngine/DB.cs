using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    class DB
    {
        Hashtable ht;

        string name;

        public DB(string name)
        {
            ht = new Hashtable();

            this.name = name;
        }

        public string runQuery(string query)
        {
            string select = @"(SELECT)\s+(\*|(\w+)|(\w+)(\,\s+(\w+))+)\s+(FROM)\s+(\w+)(\;|\s+(WHERE)\s+(\w+)\s*(=|<|>)\s*(\w+);)";
            string insert = @"(INSERT)\s+(INTO)\s+(\w+)(\s+|\s+\((\w+)\)\s+|\s+\((\w+)(\,\s+(\w+))+\))\s+(VALUES)(\s+\((\w+)\)|\s+\((\w+)(\,\s+(\w+))+\));";
            string delete = @"(DELETE)\s + (FROM)\s + (\w +)\s + (WHERE)\s +\w +\s * (=|<|>)\s *\w +;";
            string update = @"(UPDATE)\s+(\w+)\s+(SET)\s+(\w+)\s*\=\s*(\w+)(\s+|\,\s+(\w+)\s*\=\s*(\w+)\s+)(WHERE)\s+(\w+)\s*(=|<|>)\s*(\w+);";
            string dropTable = @"(DROP)\s+(TABLE)\s+(\w+);";
            string dropDB = @"(DROP)\s+(DATABASE)\s+(\w+);";
            string createDB = @"(CREATE)\s+(DATABASE)\s+(\w+);";
            string backupDB = @"(BACKUP\s+DATABASE)\s+(\w+)\s+TO\s+DISK\s*\=\s*\'([^\']+)\';";
            string createTable = @"(CREATE\s+TABLE)\s+(\w+)\s+\((\w+\s+(INT|DOUBLE|TEXT)(\s+|\,\s+\w+\s+(INT|DOUBLE|TEXT))+)\,\s+(PRIMARY\s+KEY)\s+\((\w+)\)\,\s+(FOREIGN\s+KEY)\s+\((\w+)\)\s+REFERENCES\s+(\w+)\s+\((\w+)\);";

            Match match1 = Regex.Match(query, select);
            Match match2 = Regex.Match(query, insert);
            Match match3 = Regex.Match(query, delete);
            Match match4 = Regex.Match(query, update);
            Match match5 = Regex.Match(query, dropTable);
            Match match6 = Regex.Match(query, dropDB);
            Match match7 = Regex.Match(query, createDB);
            Match match8 = Regex.Match(query, backupDB);
            Match match9 = Regex.Match(query, createTable);

            if(match1.Success)
            {
                match1.
            }
            else if(match2.Success)
            {

            }
            else if (match3.Success)
            {

            }
            else if (match4.Success)
            {

            }
            else if (match5.Success)
            {

            }
            else if (match6.Success)
            {

            }
            else if (match7.Success)
            {

            }
            else if (match8.Success)
            {

            }
            else if (match9.Success)
            {

            }

            return null;
        }
    }
}
