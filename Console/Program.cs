using MiniSQLEngine;
using System;

namespace Programa
{
    class Program
    {

        static void Main(string[] args)
        {
            DB db = new DB("db1");
            
           // Console.WriteLine(abc[0]);
            //val = Console.ReadLine();
            
            string linea ="";
            System.IO.StreamReader file = new System.IO.StreamReader(@"..\..\..\Archivos\TesterInput-example.txt");
            
            while (linea != null )
            {
                linea = file.ReadLine();
                if (linea != "" && linea != null)
                {
                    Console.WriteLine(db.runQuery(linea));
                }
            }

            Console.WriteLine("Querys Finished");
        }
    }
}