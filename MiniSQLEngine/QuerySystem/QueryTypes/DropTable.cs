using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine.QuerySystem.QueryTypes
{
    public class DropTable : SQLtype
    {
        private string table;


        public override string Execute(DB database)
        {
            return database.dropTable(table);
        }

        public DropTable(string table)
        {
            this.table = table;
        }
        public string getTabla()
        {
            return table;
        }
    }
}
