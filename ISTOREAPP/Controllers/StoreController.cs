using ISTOREAPP.Data.Context;
using ISTOREAPP.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ISTOREAPP.Controllers
{
    public class StoreController : Controller
    {
        private readonly StoreContext _context;

        public StoreController(StoreContext context)
        {
            _context = context;
        }
        public IActionResult StorePage(int? categoryid)
        {


            var products = _context.Products.ToList();
            var categories = _context.Categories.ToList(); // Kategoriler listesi

            var viewModel = new StoreViewModel
            {
                products = products,
                Categories = categories
            };

            return View(viewModel);
        }




    }
}
