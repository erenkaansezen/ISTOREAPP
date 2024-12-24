using ISTOREAPP.Data.Entities;
using ISTOREAPP.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ISTOREAPP.Controllers
{
    public class UserController:Controller
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        private SignInManager<AppUser> _signInManager; 

        public UserController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async  Task<IActionResult>Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    await _signInManager.SignOutAsync(); // tarayacısından cookie'leri siliyoruz
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe,true);
                    if (result.Succeeded)
                    {
                        await _userManager.ResetAccessFailedCountAsync(user);
                        await _userManager.SetLockoutEndDateAsync(user, null);

                        return RedirectToAction("Index", "Home");
                    }                    
                    else if (result.IsLockedOut)
                    {
                        var lockoutDate = await _userManager.GetLockoutEndDateAsync(user);
                        var timeLeft = lockoutDate.Value - DateTime.UtcNow;
                        ModelState.AddModelError("", $"Hesabınız Kitlendi, Lütfen {timeLeft.Minutes} dakika sonra deneyiniz"); // hatayı ekrana çıkarır                    
                    }

                }
                else
                {
                        ModelState.AddModelError("","Hatalı Email Veya Parola"); // hatayı ekrana çıkarır                    
                }
            }
            return View(model);
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

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }


    }
}
