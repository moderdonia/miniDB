using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine.QuerySystem.QueryTypes
{
    public class Update : SQLtype
    {
        private string table;
        //private string database;
        private string[] attb;         //columns
        private string[] conds;    //where

        public override string Execute(DB database)
        {
            throw new NotImplementedException();
        }

        public Update(string table, string[] attb, string[] conds)
        {
            this.table = table;
            //this.database = database;
            this.attb = attb;
            this.conds = conds;
        }
        public string getTabla()
        {
            return table;
        }
        public string[] getAttb()
        {
            return attb;
        }
        public string[] getConds()
        {
            return conds;
        }

    }
}
