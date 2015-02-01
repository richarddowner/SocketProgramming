using System;
using System.IO;
using System.Net.Sockets;

namespace EmployeeTCPClient
{
    internal static class EmployeeTCPClient
    {
        public static void Main(string[] args)
        {
            var hostname = GetHostname(args);

            var client = new TcpClient(hostname, 2055);
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

        private static string GetHostname(string[] args)
        {
            string hostname;
            if (args.Length != 0)
            {
                hostname = args[0];
            }
            else
            {
                Console.Write("Please enter server hostname: ");
                hostname = Console.ReadLine();
            }
            return hostname;
        }
    }
}