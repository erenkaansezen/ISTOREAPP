using ISTOREAPP.Models;
using ISTOREAPP.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ISTOREAPP.Controllers
{
    public class PageManagementController : Controller
    {
        private readonly StoreContext _context;

        public PageManagementController(StoreContext context)
        {
            _context = context;
        }
        public IActionResult HomePageManagement()
        {
            return View(_context.Sliders);
        }
        public IActionResult StorePageManagement()
        {
            return View();
        }
        public IActionResult ContactPageManagement()
        {
            return View();
        }

        public IActionResult HomeSliderManagement()
        {
            // Veritabanından Slider verilerini al
            var sliders = _context.Sliders.ToList();



            // Dönüştürülen modeli view'a gönder
            return View(sliders);
        }
        public IActionResult HomeCategoryManagement()
        {
            return View();
        }
        public IActionResult HomeFeaturedManagement()
        {
            return View();
        }

    }
}
