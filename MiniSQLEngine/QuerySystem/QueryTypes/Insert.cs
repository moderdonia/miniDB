using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine.QuerySystem.QueryTypes
{
    class Insert : SQLtype
    {
        private string table;
        //private string database;
        private string[] attb;         //columns
        private string[] values;    

        public override string Execute(DB database)
        {
            throw new NotImplementedException();
        }

        public Insert(string table, string[] attb, string[] values)
        {
            this.table = table;
            //this.database = database;
            this.attb = attb;
            this.values = values;
        }
    }
}
