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
        private Boolean esta(string[] pNombres,string pName)
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
            
            try
                {
                string fileName = @"..\..\..\Archivos\" + table + ".txt";
                string ruta = @"..\..\..\Archivos\";
                string aux = "";
                string[] nombres = Directory.GetFiles(ruta);
                bool existe = esta(nombres, fileName);
                Console.WriteLine(existe);
                if (!File.Exists(fileName))
                 {
                     File.Create(fileName);
                 }
                 
                foreach (string s in column)
                    {
                    if (s != null)
                    {
                        aux += s + ";";
                    }
                       
                    }
                    aux += Environment.NewLine;
                    File.WriteAllText(fileName, aux);
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
