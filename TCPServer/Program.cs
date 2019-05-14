using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace TCPClient
{
    class Program
    {
        static void Main(string[] args)
        {
            const string argPrefixIp = "ip=";
            const string argPrefixPort = "port=";

            string ip = "192.168.0.15";
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
                byte[] outputBuffer = new byte[1024];
                byte[] inputBuffer = new byte[1024];
                byte[] endMessage = Encoding.ASCII.GetBytes("END");


                while (request!="END")
                {
                    int readBytes = networkStream.Read(inputBuffer, 0, 1024);
                    Console.WriteLine("Response received: " + Encoding.ASCII.GetString(inputBuffer, 0, readBytes));

                    request = Console.ReadLine();
                    outputBuffer = Encoding.ASCII.GetBytes(request);
                    networkStream.Write(outputBuffer, 0, outputBuffer.Length);
                    
                    Thread.Sleep(2000);
                }
                networkStream.Write(endMessage, 0, endMessage.Length);
            }
        }
    }
}
