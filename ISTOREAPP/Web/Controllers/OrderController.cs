using Business.Services;
using ISTOREAPP.Data.Context;
using ISTOREAPP.Data.Entities;
using ISTOREAPP.Helpers;
using ISTOREAPP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ISTOREAPP.Web.Controllers
{
    public class OrderController:Controller
    {
        public StoreContext _storeContext;

        public Cart _cart;
        private readonly UserManager<AppUser> _userManager;


        public OrderController(Cart cartService, UserManager<AppUser> userManager, StoreContext storeContext)
        {
            _cart = cartService;
            _userManager = userManager;
            _storeContext = storeContext;
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
            var user = _userManager.GetUserAsync(User).Result; // Oturum açmış kullanıcıyı alıyoruz.

            var carted = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();

            if (carted.Items.Count == 0)
            {
                ModelState.AddModelError("", "Sepetinizde ürün yok!");            
            }
            if (ModelState.IsValid)
            {
                var order = new Order
                {
                    Name = user.UserName,
                    Email = user.Email,
                    City = model.City,
                    Phone = user.PhoneNumber,
                    AddressLine = model.AdressLine,
                    OrderDate = DateTime.Now,
                    OrderItems = carted.Items.Select(i => new OrderItem
                    {
                        ProductId = (int)i.Product.Id,
                        Price = (double)i.Product.Price,
                        Quantity = i.Quantity
                    }).ToList()
                };
                // Order'ı Orders tablosuna ekliyoruz
                _storeContext.Orders.AddAsync(order);  // `Orders` tablosuna ekliyoruz

                // Veritabanına kaydediyoruz
                _storeContext.SaveChangesAsync();  // Veritabanına işlemi kaydediyoruz

                // Sepeti temizleyebiliriz (isteğe bağlı)
                HttpContext.Session.Remove("cart");
                return RedirectToPage("/Completed");
            }
            else
            {
                return RedirectToPage("/StorePage");

            }

        }


    }
}
