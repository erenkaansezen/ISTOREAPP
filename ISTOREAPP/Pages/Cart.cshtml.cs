using Business.Services;
using ISTOREAPP.Data.Context;
using ISTOREAPP.Data.Entities;
using ISTOREAPP.Helpers;
using ISTOREAPP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;

namespace ISTOREAPP.Pages
{
    public class CartModel : PageModel
    {
        
        private StoreContext _storeContext;
        private ProductService _productService;
        public CartModel( ProductService productService, StoreContext storeContext,Cart cartService)
        {
            _storeContext = storeContext;
            _productService = productService;
            Cart = cartService;
        }

        public Cart? Cart { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost(int Id)
        {
            var product = _storeContext.Products.FirstOrDefault(p => p.Id == Id);
            if (product != null)
            {
                Cart?.AddItem(product, 1);

            }

            return RedirectToAction("StorePage", "Store");
        }

        public IActionResult OnAddItem(int Id)
        {
            var product = _storeContext.Products.FirstOrDefault(p => p.Id == Id);
            if (product != null)
            {
                Cart?.AddItem(product, 1);

            }

            return RedirectToPage("/Cart");
        }
        public IActionResult OnPostDecrease(int Id,string action)
        {
            var product = _storeContext.Products.FirstOrDefault(p => p.Id == Id);
            if (product != null)
            {

                Cart?.ItemDecrease(product, 1);

            }

            return RedirectToPage("/Cart");


        }

    }
}
