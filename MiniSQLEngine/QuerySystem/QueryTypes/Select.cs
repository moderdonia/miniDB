using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine.QuerySystem.QueryTypes
{
    class Select : SQLtype
    {
        public string table { get; set; }
        public string[] attb { get; set; }         //columns
        public string[] conds { get; set; }    //where

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
