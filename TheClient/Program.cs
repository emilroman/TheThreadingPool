using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TheClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("How many simultaneous requests shall we make today?");

            var requests = int.Parse(Console.ReadLine());

            var listOfActions = new List<Action>();
            for (int i = 0; i < requests; i++)
            {
                var client = new HttpClient();
                listOfActions.Add(() => { client.GetAsync("http://localhost:5000/home/index"); });
            }

            var options = new ParallelOptions { MaxDegreeOfParallelism = requests};

            while (true)
            {
                Parallel.Invoke(options, listOfActions.ToArray());
            }
        }
    }
}
