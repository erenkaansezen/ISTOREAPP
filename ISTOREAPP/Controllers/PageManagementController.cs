using Microsoft.AspNetCore.Mvc;

namespace ISTOREAPP.Controllers
{
    public class PageManagementController : Controller
    {
        public IActionResult HomePageManagement()
        {
            return View();
        }
        public IActionResult StorePageManagement()
        {
            return View();
        }
        public IActionResult ContactPageManagement()
        {
            return View();
        }
    }
}
