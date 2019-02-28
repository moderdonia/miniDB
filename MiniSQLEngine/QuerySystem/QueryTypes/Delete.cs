using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine.QuerySystem.QueryTypes
{
    class Delete : SQLtype
    {
        private string table;
        //private string database;
        private string[] conds;    //where

        public override string Execute(DB database)
        {
            throw new NotImplementedException();
        }

        public Delete(string table, string[] conds)
        {
            this.table = table;
            //this.database = database;
            this.conds = conds;
        }

        public string getTabla()
        {
            return table;
        }
        public string[] getCond()
        {
            return conds;
        }
    }
}
