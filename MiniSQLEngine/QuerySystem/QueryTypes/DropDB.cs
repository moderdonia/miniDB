using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine.QuerySystem.QueryTypes
{
    class DropDB : SQLtype
    {
        private string database;

        

        public DropDB(string database)
        {
            this.database = database;
        }

        public override string Execute(DB database)
        {
            throw new NotImplementedException();
        }
        public string getDB()
        {
            return database;
        }
    }
}
