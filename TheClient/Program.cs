using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TheClient
{
    public class Program
    {
        private static HttpClient _httpClient = new HttpClient();
        private static List<Task> _concurrentRequests;
        private const string ApiUrl = "http://localhost:5000/home/index";

        static void Main(string[] args)
        {
            Console.WriteLine("How many simultaneous requests shall we make?");
            var requests = int.Parse(Console.ReadLine());

            _httpClient = new HttpClient();
            _concurrentRequests = new List<Task>();

            for (int i = 0; i < requests; i++)
            {
                _concurrentRequests.Add(Task.Factory.StartNew(CreateRequest));
            }
            LogRequestsNumber();

            while (true)
            {

                if (Console.ReadKey().Key == ConsoleKey.UpArrow)
                {
                    _concurrentRequests.Add(Task.Factory.StartNew(CreateRequest));
                    LogRequestsNumber();
                };
            }
        }

        private static Task CreateRequest()
        {
            return _httpClient.GetAsync(ApiUrl).ContinueWith(task => CreateRequest());
        }
        
        private static void LogRequestsNumber()
        {
            Console.WriteLine($"Number of parallel request: {_concurrentRequests.Count}");
        }
    }
}
