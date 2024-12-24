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
            // Veritaban�ndan Slider ve Category verilerini al
            var sliders = _context.Sliders.ToList();
            var categories = _context.Categories.ToList();

            // HomeViewModel olu�tur ve verileri ekle
            var model = new HomeViewModel
            {
                Sliders = sliders,
                Categories = categories
            };

            // View'a model g�nder
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
