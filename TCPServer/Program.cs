using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text.RegularExpressions;

namespace TCPClient
{
    class Program
    {
        static void Main(string[] args)
        {

            string login = @"(\w+),(\w+),(\w+)";

            const string argPrefixIp = "ip=";
            const string argPrefixPort = "port=";

            string ip = ""; //poner la ip de vuestro ordenador
            int port = 1080;

            foreach (string arg in args)
            {
                if (arg.StartsWith(argPrefixIp)) ip = arg.Substring(argPrefixIp.Length);
                else if (arg.StartsWith(argPrefixPort)) port = int.Parse(arg.Substring(argPrefixPort.Length));
            }
            if (ip == null || port == 0)
            {
                Console.WriteLine("ERROR. Usage: TCPClient ip=<ip> port=<port>");
                return;
            }

            using (TcpClient client = new TcpClient(ip, port))
            {
                NetworkStream networkStream = client.GetStream();
                string request = "";
                string request2 = "";
                int cont = 0;
                byte[] outputBuffer = new byte[1024];
                byte[] inputBuffer = new byte[1024];
                byte[] endMessage = Encoding.ASCII.GetBytes("END");


                while (request!="END")
                {
                    int readBytes = networkStream.Read(inputBuffer, 0, 1024);
                    Console.WriteLine("Response received: " + Encoding.ASCII.GetString(inputBuffer, 0, readBytes));

                    request = Console.ReadLine();

                    //parsear la sentencia
                   
                        if (cont == 0)
                        {
                            request2 = "<Open Database=" + "\u0022" + request + "\u0022";
                            cont++;
                        }
                        else if (cont == 1)
                        {
                            request2 += " User=" + "\u0022" + request + "\u0022";
                            cont++;
                        }
                        else if (cont == 2)
                        {
                            request2 += " Password=" + "\u0022" + request + "\u0022" + "/>";
                        }
                    outputBuffer = Encoding.ASCII.GetBytes(request2);
                    networkStream.Write(outputBuffer, 0, outputBuffer.Length);

                    Thread.Sleep(2000);

                }
                
                networkStream.Write(endMessage, 0, endMessage.Length);
            }
        }
    }
}
