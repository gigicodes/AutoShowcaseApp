using System.Diagnostics;
using AutoShowcaseApp.Models;
using AutoShowcaseApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AutoShowcaseApp.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly ICarRepository _cars;

        public HomeController(ICarRepository cars) => _cars = cars;

        public IActionResult Index()
        {
            var allAvailable = _cars.GetAvailable();
            ViewBag.TotalCars = allAvailable.Count; 

            return View();
        }

        public IActionResult Privacy()
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
