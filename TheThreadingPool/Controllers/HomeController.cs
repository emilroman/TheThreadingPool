using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TheThreadingPool.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            await Task.Delay(2000);
            
            return Ok("Awesome!!!");
        }
    }
}