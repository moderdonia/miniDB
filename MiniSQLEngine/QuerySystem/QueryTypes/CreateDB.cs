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

        bool Execute()
        {
            throw new NotImplementedException();
        }

        public CreateDB(string database)
        {
            this.database = database;
        }
    }
}
