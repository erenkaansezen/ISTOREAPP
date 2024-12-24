using ISTOREAPP.Models;
using ISTOREAPP.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISTOREAPP.Controllers
{
    public class HomeController : Controller
    {
        private readonly StoreContext _context;

        public HomeController(StoreContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Veritabanýndan Slider ve Category verilerini al
            var sliders = _context.Sliders.ToList();
            var categories = _context.Categories.ToList();

            // HomeViewModel oluþtur ve verileri ekle
            var model = new HomeViewModel
            {
                Sliders = sliders,
                Categories = categories
            };

            // View'a model gönder
            return View(model);
        }

        [Authorize]
        public IActionResult Product()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }

    }
}
