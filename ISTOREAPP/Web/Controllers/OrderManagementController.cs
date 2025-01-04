using Business.Services;
using ISTOREAPP.Data.Context;
using ISTOREAPP.Data.Entities;
using ISTOREAPP.ViewModels;
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
        public async Task<IActionResult> ReceivedOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return View(orders);
        }



        public async Task<IActionResult> PendingOrders()
        {
            var orders = await _orderService.GetApproveTrue();
            return View(orders);
        }


        [HttpPost]
        public async Task<IActionResult> ApproveOrders(int id)
        {
            await _orderService.Approve(id);
            return RedirectToAction("ReceivedOrders");

        }

        [HttpPost]
        public async Task<IActionResult> OrdersDelete(int id)
        {
            await _orderService.DeleteOrderAsync(id);
            return RedirectToAction("ReceivedOrders");

        }
    }
}
