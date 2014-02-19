using System;
using System.IO;
using System.Net.Sockets;

namespace EmployeeTCPClient
{
    internal class EmployeeTCPClient
    {
        public static void Main(string[] args)
        {
            var client = new TcpClient(args[0], 2055);
            try
            {
                Stream s = client.GetStream();
                var sr = new StreamReader(s);
                var sw = new StreamWriter(s)
                {
                    AutoFlush = true
                };
                Console.WriteLine(sr.ReadLine());
                while (true)
                {
                    Console.Write("Name: ");
                    string name = Console.ReadLine();
                    sw.WriteLine(name);
                    if (name == "")
                    {
                        break;
                    }
                    Console.WriteLine(sr.ReadLine());
                }
                s.Close();
            }
            finally
            {
                client.Close();
            }
        }
    }
}