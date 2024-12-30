using ISTOREAPP.Models;
using Microsoft.AspNetCore.Mvc;

namespace ISTOREAPP.Components
{
    public class CartSummaryViewComponent: ViewComponent
    {
        private Cart _cart;
        public CartSummaryViewComponent(Cart CartService)
        {
            _cart = CartService;
        }

        public IViewComponentResult Invoke()
        {
            return View(_cart);
        }
    }
}
