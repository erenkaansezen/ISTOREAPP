using ISTOREAPP.Data.Entities;
using ISTOREAPP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ISTOREAPP.Web.Controllers
{
    public class OrderController:Controller
    {
        public Cart _cart;
        private readonly UserManager<AppUser> _userManager;

        public OrderController(Cart cart, UserManager<AppUser> userManager)
        {
            _cart = cart;
            _userManager = userManager;
        }
        public IActionResult Checkout()
        {
            return View(new OrderModel() { Cart = cart });
        }

        [HttpPost]
        public async IActionResult Checkout(OrderModel model)
        {
            if (_cart.Items.Count == 0)
            {
                ModelState.AddModelError("", "Sepetinizde ürün yok.");
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var order = new Order
                {
                    User = model.User,
                    Email = model.,
                    City = model.City,
                    Phone = model.Phone,
                    AddressLine = model.AddressLine,
                    OrderDate = DateTime.Now,
                    OrderItems = cart.Items.Select(i => new OrderItem
                    {
                        ProductId = i.Product.Id,
                        Price = (double)i.Product.Price,
                        Quantity = i.Quantity
                    }).ToList()
                };
                _orderRepository.SaveOrder(order);
                cart.Clear();
                return RedirectToPage("/Completed", new { OrderId = order.Id });
            }
            else
            {
                model.Cart = cart;
                return View(model);
            }
        }
    }
}
