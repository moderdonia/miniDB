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

        bool Execute(DB database)
        {
            throw new NotImplementedException();
        }

        public DropDB(string database)
        {
            this.database = database;
        }
    }
}
