using System;
using System.Threading;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace TheThreadingPool
{
    public class Program
    {
        public static int Requests;

        public static void Main(string[] args)
        {
            new Thread(ShowThreadStats)
            {
                IsBackground = true
            }.Start();
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://*:5000")
                .ConfigureLogging(logging =>
                {
                    logging.SetMinimumLevel(LogLevel.Critical);

                })
                .Build();

        private static void ShowThreadStats(object obj)
        {
            ThreadPool.SetMaxThreads(10, 100);

            while (true)
            {
                ThreadPool.GetAvailableThreads(out var workerThreads, out var _);
                ThreadPool.GetMinThreads(out var minThreads, out int _);
                ThreadPool.GetMaxThreads(out var maxThreads, out int _);
                
                Console.WriteLine($"Available: {workerThreads}, Active: {maxThreads-workerThreads}, MinThreads: {minThreads}, MaxThreads: {maxThreads}, Requests: {Requests}");

                Thread.Sleep(1000);
            }
        }
    }
}
