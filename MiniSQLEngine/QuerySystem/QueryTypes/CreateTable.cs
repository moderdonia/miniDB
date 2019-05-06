using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine.QuerySystem.QueryTypes
{
    public class CreateTable : SQLtype
    {
        private string table;
        private string[] column;

        public override string Execute(DB database)
        {
            return database.createTable(table, column);
        }
        private bool esta(string[] pNombres,string pName)
        {
            int i = 0;
            while (i <= pNombres.Length)
            {
                if(pName == pNombres[i])
                {
                    return true;
                }
                i++;
            }
            return false;
        }
        public CreateTable(string table, string[] column)
        {
            //string fileName = @"..\..\Files\tabla.txt";
                this.table = table;
                this.column = column;
        }
        public string getTabla()
        {
            return table;
        }
        public string[] getColumn()
        {
            return column;
        }
    }
}
