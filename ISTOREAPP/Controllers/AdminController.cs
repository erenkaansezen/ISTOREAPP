using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ISTOREAPP.Controllers
{
    public class AdminController:Controller
    {
        private UserManager<IdentityUser> _userManager;
        public AdminController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult UsersList()
        {
            return View(_userManager.Users);
        }
        public IActionResult UserCreate()
        {
            return View();
        }
    }
}
