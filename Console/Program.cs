using MiniSQLEngine;
using System;
using System.Diagnostics;

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
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    string output = db.runQuery(linea) + "(";
                    output += sw.Elapsed.TotalMilliseconds + ")";
                    Console.WriteLine(output);
                    sw.Stop();
                }
            }
            Console.WriteLine("Querys Finished");
        }
    }
}