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
            string[] lineArray = new string[3];
            List<string> dbList = new List<string>();
            Profiles prof = Profiles.getInstance();
            DB database;

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
                    byte[] outputBuffer = new byte[1024];
                    NetworkStream networkStream = client.GetStream();

                    //Read message from the client
                    int size = 1024 ;
                    string request = "";
                    size = networkStream.Read(inputBuffer, 0, 1024);
                    request += Encoding.ASCII.GetString(inputBuffer, 0, size);
                    DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_CLOSE, MF_BYCOMMAND);
                    line = DesParser(request);
                    lineArray = line.Split(',');

                    if (lineArray[1].Equals("admin") && lineArray[2]=="admin" && !dbList.Contains(lineArray[0]))
                    {
                        
                        //new CreateDB(lineArray[0], lineArray[1], lineArray[2]);
                        dbList.Add(lineArray[0]);
                        //DB db = new DB(line);



                        //********************************* aqui
                        using (DB db = new DB(lineArray[0])) //desde aqui
                        {
                            db.user = lineArray[1];
                            database = db;
                            prof.SetDB(db);
                            //bool bucle = true;
                            //string linea;

                            string fileName = @"..\..\..\Archivos\";
                            if (!Directory.Exists(fileName))
                            {
                                System.IO.Directory.CreateDirectory(fileName);
                            }
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
                                    if (nom == "secProfiles")
                                    {
                                        while (!archivo.EndOfStream)
                                        {
                                            string name;
                                            string pass;
                                            string secProfile;
                                            string table;
                                            List<bool> booleans = new List<bool>();
                                            row = archivo.ReadLine();
                                            string[] a = row.Split(';');
                                            name = a[0];
                                            pass = a[1];
                                            secProfile = a[2];
                                            table = a[3];
                                            booleans.Add(bool.Parse(a[4]));
                                            booleans.Add(bool.Parse(a[5]));
                                            booleans.Add(bool.Parse(a[6]));
                                            booleans.Add(bool.Parse(a[7]));
                                            MiniSQLEngine.Profiles.getInstance().AddProfile(name, pass, secProfile, table, booleans);
                                        }
                                    }
                                    else
                                    {
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

                                    }
                                }
                                i++;
                            }
                        } //hasta aqui es la carga de datos (esto estaba abajo *******)


                        //bool bucle = true;
                        //string linea;
                        //no se puede cerrar pulsando la X


                        while (request != "END")
                        {
                            outputBuffer = Encoding.ASCII.GetBytes("\nInserte sentencia o escriba 'END' para salir");
                            networkStream.Write(outputBuffer, 0, outputBuffer.Length);

                            size = networkStream.Read(inputBuffer, 0, 1024);
                            request = Encoding.ASCII.GetString(inputBuffer, 0, size);

                            DesParser(request);

                            Console.WriteLine("Request received: " + request);

                            Stopwatch sw = new Stopwatch();
                            sw.Start();
                            string output = "<Answer>" + database.runQuery(request) + "</Answer>" + "(";
                            output += sw.Elapsed.TotalMilliseconds + ")";
                            Console.WriteLine(output);
                            sw.Stop();
                            outputBuffer = Encoding.ASCII.GetBytes(output);
                            networkStream.Write(outputBuffer, 0, outputBuffer.Length);
                        }
                        client.Close();
                        
                    }
                    else
                    {

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
        //-------------------------------------------------------------------------------
        }
        private static string DesParser(string query)
        {

            //CreateDBXML
            string createXML = @"<Open\s*Database\=\'(\w+)\'\s*User\=\'(\w+)\'\s*Password\=\'(\w+)\'\/>;";

            //QueryXML
            string queryXML = @"<Query\>([\w+\s*\*\=]+\;)\<\/Query\>";

            string queryX = "";

            Match matchXML = Regex.Match(query, queryXML);
            Match matchXML2 = Regex.Match(query, createXML);

            if (matchXML.Success)
            {
                queryX = matchXML.Groups[1].Value;
            }
            else if (matchXML2.Success)
            {
                queryX = matchXML2.Groups[1].Value;
                queryX += "," + matchXML2.Groups[2].Value;
                queryX += "," + matchXML2.Groups[3].Value;
            }
            return queryX;
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
    }          
}
