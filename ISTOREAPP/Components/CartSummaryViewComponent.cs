using ISTOREAPP.Helpers;
using ISTOREAPP.Models;
using Microsoft.AspNetCore.Mvc;

namespace StoreApp.Web.Components;

public class CartSummaryViewComponent : ViewComponent
{
    private Cart cart;

    public CartSummaryViewComponent(Cart cartSevice)
    {
        cart = cartSevice;
    }

    public IViewComponentResult Invoke()
    {
        // Sepet ürün adedini hesaplayın
        int totalItems = cart.Items.Sum(i => i.Quantity);

        // Bu değeri View'a gönderelim
        ViewData["TotalItems"] = totalItems;

        return View(cart);
    }
}