using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine.QuerySystem.QueryTypes
{
    public class Insert : SQLtype
    {
        private string table;
        //private string database;
        private string[] attb;         //columns
        private string[] values;    

        public override string Execute(DB database)
        {
 
            return database.insertData(table,attb,values);
        }

        public Insert(string table, string[] attb, string[] values)
        {
            this.table = table;
            //this.database = database;
            this.attb = attb;
            this.values = values;
        }
        public string getTabla()
        {
            return table;
        }
        public string[] getAttb()
        {
            return attb;
        }
        public string[] getValues()
        {
            return values;
        }
    }
}
