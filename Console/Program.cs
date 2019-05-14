using MiniSQLEngine;
using MiniSQLEngine.QuerySystem.QueryTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Programa
{
    class Program
    {

        static void Main(string[] args)
        {

            string line="";
            string line2="";
            string line3="";
            List<string> dbList = new List<string>();


            const string argPrefixPort = "port=";

            int port = 1080;
            foreach (string arg in args)
            {
                if (arg.StartsWith(argPrefixPort)) port = int.Parse(arg.Substring(argPrefixPort.Length));
            }
            if (port == 0)
            {
                Console.WriteLine("ERROR. Usage: TCPClient ip=<ip> port=<port>");
                return;
            }

            TcpListener listener = new TcpListener(IPAddress.Any, port);
            listener.Start();

            Console.WriteLine("Server listening for clients");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Client connection accepted");

                var childSocketThread = new Thread(() =>
                {
                    
                    byte[] inputBuffer = new byte[1024];
                    NetworkStream networkStream = client.GetStream();

                    //Read message from the client
                    int size = 1024 ;
                    string request = "";

                    byte[] outputBuffer = Encoding.ASCII.GetBytes("What database you wanna open bro?");
                    networkStream.Write(outputBuffer, 0, outputBuffer.Length);

                    size = networkStream.Read(inputBuffer, 0, 1024);
                    request = Encoding.ASCII.GetString(inputBuffer, 0, size);

                    line = request;

                    outputBuffer = Encoding.ASCII.GetBytes("What's your name mate?");
                    networkStream.Write(outputBuffer, 0, outputBuffer.Length);

                    size = networkStream.Read(inputBuffer, 0, 1024);
                    request = Encoding.ASCII.GetString(inputBuffer, 0, size);

                    line2 = request;

                    outputBuffer = Encoding.ASCII.GetBytes("And your password dude?");
                    networkStream.Write(outputBuffer, 0, outputBuffer.Length);

                    size = networkStream.Read(inputBuffer, 0, 1024);
                    request = Encoding.ASCII.GetString(inputBuffer, 0, size);

                    line3 = request;

                    if (!line2.Equals("admin") && !line3.Equals("admin"))
                    {
                        Console.WriteLine("Error: Not sufficient privileges");

                    }
                    else if (!dbList.Contains(line))
                    {
                        new CreateDB(line, line2, line3);
                        dbList.Add(line);
                        //DB db = new DB(line);

                        //bool bucle = true;
                        //string linea;
                        //no se puede cerrar pulsando la X
                        DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_CLOSE, MF_BYCOMMAND);
                        using (DB db = new DB(line))
                        {
                            Profiles prof = Profiles.getInstance();
                            prof.SetDB(db);
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
                                        if (k == 0)
                                        {
                                            columnas = row.Split(';');
                                            db.createTable(nom, columnas);
                                            k++;
                                        }
                                        else
                                        {

                                            db.insertData(nom, columnas, row.Split(';'));
                                        }
                                    }
                                    //codigo para lectura con pattern
                                    //-------------------------------
                                    i++;
                                }
                            }
                            while (request != "END")
                            {
                                outputBuffer = Encoding.ASCII.GetBytes("Inserte sentencia o escriba 'exit' para salir");
                                networkStream.Write(outputBuffer, 0, outputBuffer.Length);

                                size = networkStream.Read(inputBuffer, 0, 1024);
                                request = Encoding.ASCII.GetString(inputBuffer, 0, size);

                                Console.WriteLine("Request received: " + request);           

                                Stopwatch sw = new Stopwatch();
                                sw.Start();
                                string output = db.runQuery(request) + "(";
                                output += sw.Elapsed.TotalMilliseconds + ")";
                                Console.WriteLine(output);
                                sw.Stop();
                                outputBuffer = Encoding.ASCII.GetBytes(output);
                                networkStream.Write(outputBuffer, 0, outputBuffer.Length);
                            }
                                client.Close();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Not sufficient privileges");
                    }
                });
                childSocketThread.Start();

                //Console.WriteLine("What database you wanna open bro?");
                //line = Console.ReadLine();

                //Console.WriteLine("What's your name mate?");
                //line2 = Console.ReadLine();

                //Console.WriteLine("And your password dude?");
                //line3 = Console.ReadLine();

                
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
