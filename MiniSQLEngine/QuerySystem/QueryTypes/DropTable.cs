using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine.QuerySystem.QueryTypes
{
    class DropTable : SQLtype
    {
        private string table;

        bool Execute()
        {
            throw new NotImplementedException();
        }

        public DropTable(string table)
        {
            this.table = table;
        }
    }
}
