using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TheThreadingPool.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("Sync")]
        public string IndexSync()
        {
            Task.Delay(2000).Wait();

            return "Awesome!!!";
        }

        [HttpGet]
        public async Task<string> IndexAsync()
        {
            await Task.Delay(2000);

            return "Awesome!!!";
        }
    }
}