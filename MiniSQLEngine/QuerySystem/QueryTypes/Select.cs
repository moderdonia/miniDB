using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine.QuerySystem.QueryTypes
{
    class Select : SQLtype
    {
        private string table;
        private string[] attb;         //columns
        private string[] conds;    //where

        public override string Execute(DB database)
        {

            throw new NotImplementedException();
        }

        public Select(string table, string[] attb, string[] conds)
        {
            this.table = table;
            this.attb = attb;
            this.conds = conds;
        }
    }
}
