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
        private Column[] attb;

        public override string Execute(DB database)
        {
            return database.addtable(table, attb);
        }

        public CreateTable(string table, Column[] attb)
        {
            this.table = table;
            this.attb = attb;

        }
        public string getTabla()
        {
            return table;
        }
        public Column[] getAttb()
        {
            return attb;
        }
    }
}
