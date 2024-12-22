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
            // Veritaban�ndan Slider verilerini al
            var sliders = _context.Sliders.ToList();



            // D�n��t�r�len modeli view'a g�nder
            return View(sliders);
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


    }
}
