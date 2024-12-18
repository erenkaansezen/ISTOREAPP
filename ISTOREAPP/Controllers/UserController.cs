using ISTOREAPP.Models;
using ISTOREAPP.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ISTOREAPP.Controllers
{
    public class UserController:Controller
    {
        private UserManager<AppUser> _userManager;
        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateViewModel model)
        {
            if (ModelState.IsValid) 
            {
                var user = new AppUser { UserName = model.Email,Email = model.Email, FullName = model.FullName}; //ıdentity ile form üzerindeki user bilgilerini eşitler

                IdentityResult result = await _userManager.CreateAsync(user , model.Password); // form'dan aldığı bilgiler ile usermanager üzerinden kullanıcı oluşturur

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home"); // eğer giriş başarılıysa kullanıcıyı home sayfasına yollar
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description); // hatayı ekrana çıkarır
                    }
                }


            }
            return View(model);
        }


    }
}
