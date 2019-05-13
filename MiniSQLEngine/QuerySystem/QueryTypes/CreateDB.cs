using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine.QuerySystem.QueryTypes
{
    public class CreateDB : SQLtype
    {
        private string database;
        private string user;
        private string password;

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
        
        public CreateDB(string database, string user, string password)
        {
            this.database = database;
            this.user = user;
            this.password = password;

        }
        public string getDB()
        {
            return database;
        }
    }
}
