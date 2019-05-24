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
            //DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_CLOSE, MF_BYCOMMAND);
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
                byte[] outputBuffer = new byte[1024];
                byte[] inputBuffer = new byte[1024];
                byte[] endMessage = Encoding.ASCII.GetBytes("END");
                string[] line1 = new string[3];

                line1 = Console.ReadLine().Split(',');
                request2 = "<Open Database=" + "'" + line1[0] + "'" + " User=" + "'" + line1[1] + "'" + " Password=" + "'" + line1[2] + "'/>";

                outputBuffer = Encoding.ASCII.GetBytes(request2);
                networkStream.Write(outputBuffer, 0, outputBuffer.Length);

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
        // Necesario para que no se cierre la ventana de comandos
        //private const int MF_BYCOMMAND = 0x00000000;
        //public const int SC_CLOSE = 0xF060;

        //[DllImport("user32.dll")]
        //public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        //[DllImport("user32.dll")]
        //private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        //[DllImport("kernel32.dll", ExactSpelling = true)]
        //private static extern IntPtr GetConsoleWindow();
    }
}
