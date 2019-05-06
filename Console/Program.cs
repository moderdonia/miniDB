using MiniSQLEngine;
using MiniSQLEngine.QuerySystem.QueryTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Programa
{
    class Program
    {

        static void Main(string[] args)
        {

            string line;
            string line2;
            string line3;
            List<string> dbList = new List<string>();

            Console.WriteLine("What database you wanna open bro?");
            line = Console.ReadLine();

            Console.WriteLine("What's your name mate?");
            line2 = Console.ReadLine();

            Console.WriteLine("And your password dude?");
            line3 = Console.ReadLine();


            if (!line2.Equals("admin") && !line3.Equals("admin"))
            {
                Console.WriteLine("Error: Not sufficient privileges");

            }
            else if (!dbList.Contains(line))
            {
                new CreateDB(line, line2, line3);
                dbList.Add(line);
                DB db = new DB(line);

                bool bucle = true;
                string linea;

                string fileName = @"..\..\..\Archivos\";
                string[] nombres = Directory.GetFiles(fileName);
                string[] columnas;
                string nombre;
                int i = 0;

                //----------Codigo para leer linea
                StreamReader archivo;
                //--------------------------------
                while (i < nombres.Length)
                {
                    nombre = nombres[i];
                    //Console.WriteLine(nombres[i]);
                    //Console.WriteLine(nombres[i].Length);
                    int aux = nombres[i].Length - 4;
                    //Console.WriteLine(aux);
                    string nom = nombres[i].Substring(18);
                    nom = nom.Replace(".txt", "");
                    //archivo = File.OpenText()
                    //Console.WriteLine(nom);
                    i++;
                }



                while (bucle)
                {
                    Console.WriteLine("Inserte sentencia");
                    linea = Console.ReadLine();
                    if (linea == "exit")
                    {
                        bucle = false;
                    }
                    else
                    {
                        //El tiempo que tarda la sentencia
                        Stopwatch sw = new Stopwatch();
                        sw.Start();

                        string output = db.runQuery(linea) + "(";
                        output += sw.Elapsed.TotalMilliseconds + ")";
                        Console.WriteLine(output);
                        sw.Stop();

                    }
                }
                Console.WriteLine("Database created");
            }
            else
            {
                Console.WriteLine("Not sufficient privileges");
            }


        }


        // catch from SQLParser
        //Profiles prof = Profiles.getInstance();

        
        

            /*
                Console.WriteLine(abc[0]);
                val = Console.ReadLine();
            
          
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
            */
        }

    }
