using Business.Services;
using ISTOREAPP.Data.Context;
using ISTOREAPP.Data.Entities;
using ISTOREAPP.ViewModels;
using ISTOREAPP.ViewModels.ISTOREAPP.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ISTOREAPP.Web.Controllers
{
    [Authorize(Roles = "Admin,Owner")]
    public class OrderManagement : Controller
    {

        private OrderService _orderService;
        public OrderManagement(OrderService orderService)
        {
            _orderService = orderService;

        }



        //Order Yönetimi

        //Bekleyen Siparişler
        public async Task<IActionResult> PendingOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return View(orders);
        }

        //Tamamlanan siparişler
        public async Task<IActionResult> CompletedOrder()
        {

            var orders = await _orderService.GetApproveTrue();
            return View(orders);
        }

        //Satılan ürünün detay sayfası
        public async Task<IActionResult> OrderDetails(int id)
        {
            return View(await _orderService.GetOrderByIdAsync(id));


        }


        //ürünü PendingOrders'dan CompletedOrder'a taşır
        [HttpPost]
        public async Task<IActionResult> ApproveOrders(int id)
        {
            await _orderService.Approve(id);
            return RedirectToAction("ReceivedOrders");

        }
    }
}
