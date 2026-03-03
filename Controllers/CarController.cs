using AutoShowcaseApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AutoShowcaseApp.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarRepository _cars;

        public CarController(ICarRepository cars) => _cars = cars;

        public IActionResult Index(string? search, decimal? minPrice, decimal? maxPrice)
        {
            var cars = _cars.GetAll();
            return View(cars);
        }
    }
}
