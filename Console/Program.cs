using MiniSQLEngine;
using System;
using System.Collections.Generic;

namespace Programa
{
    class Program
    {
        
        static void Main(string[] args)
        {
            DB db = new DB("db1");
            List<string> abc = new List<string>();
            abc.Add("hola");
            abc.Add("hola2");
            abc.Add("hola3");

            //string val;
            Console.WriteLine(abc[0]);
            //val = Console.ReadLine();
            //db.runQuery(val);
            //System.IO.StreamReader file= new System.IO.StreamReader();  
            //while(){
                    
            //}
        }
    }
}