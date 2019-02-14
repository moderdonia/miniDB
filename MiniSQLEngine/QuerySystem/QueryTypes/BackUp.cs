using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine.QuerySystem.QueryTypes
{
    class BackUp : SQLtype
    {
        private string database;
        private string filepath;

        bool Execute()
        {
            throw new NotImplementedException();
        }

        public BackUp(string database, string filepath)
        {
            this.database = database;
            this.filepath = filepath;
        }
    }
}
