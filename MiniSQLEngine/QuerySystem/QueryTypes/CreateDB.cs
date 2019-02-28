using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine.QuerySystem.QueryTypes
{
    class CreateDB : SQLtype
    {
        private string database;

        public override string Execute(DB database)
        {
            System.IO.DirectoryInfo di = System.IO.Directory.CreateDirectory("../../SGBD/"+database);
            if (di.Exists)
            {
                return null;
            }
            else
            {
                return null;
            }
        
        }

        public CreateDB(string database)
        {
            this.database = database;
        }
        public string getDB()
        {
            return database;
        }
    }
}
