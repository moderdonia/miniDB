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
        private string[] attb;

        public override string Execute(DB database)
        {
            throw new NotImplementedException();
        }

        public CreateTable(string table, string[] attb)
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
