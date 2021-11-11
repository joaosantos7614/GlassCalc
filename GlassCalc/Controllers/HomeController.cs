using GlassCalc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GlassCalc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(VidroCalc vidrocalc)
        {
            vidrocalc.ValidateInputs();
            return View(vidrocalc);
        }
        [HttpGet]
        public IActionResult IndexPT()
        {
            return View();
        }
        [HttpPost]
        public IActionResult IndexPT(VidroCalc vidrocalc)
        {
            vidrocalc.ValidateInputs();
            return View(vidrocalc);
        }
        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
