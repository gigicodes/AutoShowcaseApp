using AutoShowcaseApp.Models;
using AutoShowcaseApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoShowcaseApp.Controllers.Api
{
    [Route("api/cars")]
    [ApiController]
    public class CarApiController : ControllerBase
    {
        private readonly ICarRepository _cars;
        public CarApiController(ICarRepository cars)
        {
            _cars = cars;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var cars = _cars.GetAll();
            return Ok(cars);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var car = _cars.GetById(id);
            if (car == null)
                return NotFound();
            return Ok(car);
        }

        [HttpPost]
        public IActionResult Submit([FromBody] Car car)
        {
            _cars.Add(car);
            return Ok(new { message = "Car added successfully.", id = car.Id });
        }
    }
}
