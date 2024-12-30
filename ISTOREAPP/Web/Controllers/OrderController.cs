using Business.Services;
using ISTOREAPP.Data.Entities;
using ISTOREAPP.Helpers;
using ISTOREAPP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ISTOREAPP.Web.Controllers
{
    public class OrderController:Controller
    {
        public Cart _cart;
        private readonly UserManager<AppUser> _userManager;


        public OrderController(Cart cartService, UserManager<AppUser> userManager)
        {
            _cart = cartService;
            _userManager = userManager;
        }

        public IActionResult Checkout()
        {
            var user = _userManager.GetUserAsync(User).Result; // Oturum açmış kullanıcıyı alıyoruz.
            var carted = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();

            var model = new OrderModel()
            {
                Cart = carted,

                User = user
                // Kullanıcı bilgisini modelde set ediyoruz.
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Checkout(OrderModel model)
        {
            var carted = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();

            if (carted.Items.Count == 0)
            {
                ModelState.AddModelError("", "Sepetinizde ürün yok!");            
            }
            if (ModelState.IsValid)
            {
                return RedirectToPage("/Completed");
            }
            else
            {
                return RedirectToPage("/StorePage");

            }

        }


    }
}
