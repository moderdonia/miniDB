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

        public CreateTable(string table, string[] column)
        {
            //string fileName = @"..\..\Files\tabla.txt";
            string fileName = @"..\..\..\Archivos\"+table+".txt";
            try
            {
                if (!File.Exists(fileName))
                {
                    File.Create(fileName);
                }
                    
            }
            catch (Exception ex)
            {

            }
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
