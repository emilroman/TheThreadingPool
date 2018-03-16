using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TheClient
{
    public class Program
    {
        private static HttpClient _httpClient = new HttpClient();
        private static List<Action> _concurrentRequests;
        private const string ApiUrl = "http://localhost:5000/home/index";

        static void Main(string[] args)
        {
            Console.WriteLine("How many simultaneous requests shall we make?");
            var requests = int.Parse(Console.ReadLine());

            _httpClient = new HttpClient();
            _concurrentRequests = new List<Action>();

            for (int i = 0; i < requests; i++)
            {
                _concurrentRequests.Add(CreateRequest);
            }
            LogRequestsNumber();

            var options = new ParallelOptions { MaxDegreeOfParallelism = requests };
            while (true)
            {

                if (Console.ReadKey().Key == ConsoleKey.UpArrow)
                {
                    _concurrentRequests.Add(CreateRequest);
                    options.MaxDegreeOfParallelism++;
                    LogRequestsNumber();
                };
                
                Parallel.Invoke(options, _concurrentRequests.ToArray());
            }
        }

        private static void CreateRequest()
        {
            _httpClient.GetAsync(ApiUrl);
        }
        
        private static void LogRequestsNumber()
        {
            Console.WriteLine($"Number of parallel request: {_concurrentRequests.Count}");
        }
    }
}
