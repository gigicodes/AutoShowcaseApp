using AutoShowcaseApp.Repositories;
using AutoShowcaseApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoShowcaseApp.Controllers
{
    public class InquiryController : Controller
    {
        private readonly IInquiryRepository _inquiries;
        private readonly ICarRepository _cars;

        public InquiryController(IInquiryRepository inquiries, ICarRepository cars)
        {
            _inquiries = inquiries;
            _cars = cars;
        }

        public IActionResult Create(int carId)
        {
            var car = _cars.GetById(carId);
            if (car == null) return NotFound();
            ViewBag.Car = car;
            return View(new Inquiry { CarId = carId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Inquiry inquiry)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Car = _cars.GetById(inquiry.CarId);
                return View(inquiry);
            }
            _inquiries.Add(inquiry);
            TempData["InquirySuccess"] = "true";
            return RedirectToAction("Index", "Car");
        }
    }
}
