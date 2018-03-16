﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TheThreadingPool.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [HttpGet]
        public string Index()
        {
            Task.Delay(2000).Wait();

            return "Awesome!!!";
        }
    }
}