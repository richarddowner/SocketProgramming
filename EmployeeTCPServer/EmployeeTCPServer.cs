﻿using System;
using System.Configuration;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace EmployeeTCPServer
{
    class EmployeeTCPServer
    {
        static TcpListener listener;
        const int LIMIT = 5; // 5 concurrent clients

        public static void Main()
        {
            listener = new TcpListener(2055);
            listener.Start();
            #if LOG
            Console.WriteLine("Server mounted, listening to port 2055");
            #endif
            for (int i = 0; i < LIMIT; i++)
            {
                var t = new Thread(Service);
                t.Start();
            }
        }
        public static void Service()
        {
            while (true)
            {
                Socket soc = listener.AcceptSocket();
                //soc.SetSocketOption(SocketOptionLevel.Socket,
                //        SocketOptionName.ReceiveTimeout,10000);
                #if LOG
                Console.WriteLine("Connected: {0}", soc.RemoteEndPoint);
                #endif
                try
                {
                    Stream s = new NetworkStream(soc);
                    var sr = new StreamReader(s);
                    var sw = new StreamWriter(s)
                    {
                        AutoFlush = true
                    };
                    sw.WriteLine("{0} Employees available", ConfigurationSettings.AppSettings.Count);
                    while (true)
                    {
                        string name = sr.ReadLine();
                        if (string.IsNullOrEmpty(name))
                        {
                            break;
                        }
                        string job = ConfigurationSettings.AppSettings[name];
                        if (job == null)
                        {
                            job = "No such employee";
                        }
                        sw.WriteLine(job);
                    }
                    s.Close();
                }
                catch (Exception e)
                {
                    #if LOG
                    Console.WriteLine(e.Message);
                    #endif
                }
                    #if LOG
                    Console.WriteLine("Disconnected: {0}", soc.RemoteEndPoint);
                    #endif
                soc.Close();
            }
        }
    }
}