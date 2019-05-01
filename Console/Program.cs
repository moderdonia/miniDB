using MiniSQLEngine;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Programa
{
    class Program
    {

        static void Main(string[] args)
        {
            //no se puede cerrar pulsando la X
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_CLOSE, MF_BYCOMMAND);
            using (DB db = new DB("db1"))
            {
                bool bucle = true;
                string linea;

                string fileName = @"..\..\..\Archivos\";
                string[] nombres = Directory.GetFiles(fileName);
                string[] columnas = new string[20];
                string nombre;
                int i = 0;

                //----------Codigo para leer linea
                StreamReader archivo;
                string row = null;
                int k = 0;
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
                    using (archivo = File.OpenText(nombre))
                    {
                        //Console.WriteLine(nom);
                        //File.Delete(fileName);
                        while (!archivo.EndOfStream)
                        {
                            row = archivo.ReadLine();
                            Console.WriteLine(row);
                            if (++k == 0)
                            {
                                break;
                            }
                        }
                        //codigo para lectura con pattern
                        columnas = row.Split(';');
                        db.createTable(nom, columnas);
                        //-------------------------------
                        i++;
                    }
                }
                while (bucle)
                {
                    Console.WriteLine("Inserte sentencia o escriba 'exit' para salir");
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
        // Necesario para que no se cierre la ventana de comandos
        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_CLOSE = 0xF060;

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        //-------------------------------------------------------------------------------
    }
}