using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine.QuerySystem.QueryTypes
{
    class CreateTable : SQLtype
    {
        private string table;

        bool Execute()
        {
            throw new NotImplementedException();
        }

        public CreateTable(string table)
        {
            this.table = table;
        }
    }
}
