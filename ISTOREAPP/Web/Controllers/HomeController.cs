using Business.Services;
using ISTOREAPP.Data.Context;
using ISTOREAPP.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ISTOREAPP.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly SliderService _sliderService;
        private readonly CategoryService _categoryService;
        private readonly ProductService _productService;


        public HomeController(SliderService sliderService, CategoryService categoryService, ProductService productService)
        {
            _productService = productService;
            _sliderService = sliderService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var sliders = await _sliderService.GetAllSlidersAsync();
            var categories = await _categoryService.GetAllCategoriesAsync();
            var products = await _productService.GetAllProductsAsync();

            var model = new HomeViewModel
            {
                Sliders = sliders,
                Categories = categories,
                Products = products
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
