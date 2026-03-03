using AutoShowcaseApp.Repositories;
using AutoShowcaseApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoShowcaseApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly ICarRepository _cars;
        private readonly IInquiryRepository _inquiries;
        private readonly IWebHostEnvironment _env;

        // HARDCODED CREDENTIALS
        private const string AdminUsername = "admin";
        private const string AdminPassword = "AutoAdmin2026!";

        public AdminController(ICarRepository cars, IInquiryRepository inquiries, IWebHostEnvironment env)
        {
            _cars = cars;
            _inquiries = inquiries;
            _env = env;
        }

        private bool IsLoggedIn => HttpContext.Session.GetString("AdminLoggedIn") == "true";
        private IActionResult RequireLogin() => RedirectToAction("Login");

        public IActionResult Login() => IsLoggedIn ? RedirectToAction("Dashboard") : View(new LoginModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginModel model)
        {
            if (model.Username == AdminUsername && model.Password == AdminPassword)
            {
                HttpContext.Session.SetString("AdminLoggedIn", "true");
                return RedirectToAction("Dashboard");
            }
            ModelState.AddModelError("", "Invalid credentials.");
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult Dashboard()
        {
            if (!IsLoggedIn) return RequireLogin();
            ViewBag.TotalCars = _cars.GetAll().Count;
            ViewBag.AvailableCars = _cars.GetAvailable().Count;
            ViewBag.TotalInquiries = _inquiries.GetAll().Count;
            ViewBag.RecentInquiries = _inquiries.GetAll().OrderByDescending(i => i.DateSubmitted).Take(5).ToList();
            return View();
        }

        public IActionResult ManageCars()
        {
            if (!IsLoggedIn) return RequireLogin();
            return View(_cars.GetAll());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCar(Car car, IFormFile? imageFile)
        {
            if (!IsLoggedIn) return RequireLogin();

            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsDir = Path.Combine(_env.WebRootPath, "images", "cars");
                Directory.CreateDirectory(uploadsDir);
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
                var filePath = Path.Combine(uploadsDir, fileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                await imageFile.CopyToAsync(stream);
                car.ImagePath = $"/images/cars/{fileName}";
            }
            else
            {
                car.ImagePath = "/images/cars/default.jpg";
            }

            _cars.Add(car);
            TempData["Success"] = "Car added successfully!";
            return RedirectToAction("ManageCars");
        }

        public IActionResult ToggleAvailability(int id)
        {
            if (!IsLoggedIn) return RequireLogin();
            var car = _cars.GetById(id);
            if (car != null)
            {
                car.IsAvailable = !car.IsAvailable;
                _cars.Update(car);
            }
            return RedirectToAction("ManageCars");
        }

        public IActionResult DeleteCar(int id)
        {
            if (!IsLoggedIn) return RequireLogin();
            _cars.Delete(id);
            TempData["Success"] = "Car deleted.";
            return RedirectToAction("ManageCars");
        }

        // ── INQUIRIES ──────────────────────────────────────────
        public IActionResult Inquiries()
        {
            if (!IsLoggedIn) return RequireLogin();
            var inquiries = _inquiries.GetAll().OrderByDescending(i => i.DateSubmitted).ToList();
            ViewBag.Cars = _cars.GetAll();
            return View(inquiries);
        }
    }
}
