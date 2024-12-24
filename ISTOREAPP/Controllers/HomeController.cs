using Business.Services;
using ISTOREAPP.Data.Context;
using ISTOREAPP.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ISTOREAPP.Controllers
{
    public class HomeController : Controller
    {
        private readonly SliderService _sliderService;
        private readonly CategoryService _categoryService;

        public HomeController(SliderService sliderService, CategoryService categoryService)
        {
            _sliderService = sliderService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult>Index()
        {
            var sliders = await _sliderService.GetAllSlidersAsync();
            var categories = await _categoryService.GetAllCategoriesAsync();

            var model = new HomeViewModel
            {
                Sliders = sliders,
                Categories = categories
            };

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
