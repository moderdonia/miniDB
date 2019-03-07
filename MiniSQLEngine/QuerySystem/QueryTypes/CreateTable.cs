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
        private Column[] column;

        public override string Execute(DB database)
        {
            return database.addtable(table, column);
        }

        public CreateTable(string table, Column[] column)
        {
            this.table = table;
            this.attb = attb;

        }
        public string getTabla()
        {
            return table;
        }
        public string[] getAttb()
        {
            return attb;
        }
    }
}
