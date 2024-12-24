using ISTOREAPP.Data.Context;
using ISTOREAPP.Data.Entities;
using ISTOREAPP.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ISTOREAPP.Web.Controllers
{
    public class AdminController : Controller
    {

        private UserManager<AppUser> _userManager;
        private readonly StoreContext _context;

        private readonly RoleManager<AppRole> _roleManager;
        public AdminController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, StoreContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        public IActionResult UsersList()
        {
            return View(_userManager.Users);
        }
        public async Task<IActionResult> UserEdit(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.FindByIdAsync(id);  // id bilgisini db'ye gönderip sonrasında id ile eşleşen user'ı çağırıyoruz

            if (user != null)
            {
                ViewBag.Roles = await _roleManager.Roles.Select(x => x.Name).ToListAsync();
                return View(
                     new EditViewModel
                     {
                         Id = user.Id,
                         FullName = user.FullName,
                         Email = user.Email,
                         SelectedRoles = await _userManager.GetRolesAsync(user) // kullanıcının ilişkilendirilmiş olduğu rolleri getirir
                     }
                    );



            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> UserEdit(string id, EditViewModel model)
        {
            if (id != model.Id)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.FullName = model.FullName;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded && !string.IsNullOrEmpty(model.Password))
                    {
                        await _userManager.RemovePasswordAsync(user);
                        await _userManager.AddPasswordAsync(user, model.Password);
                    }
                    if (result.Succeeded)
                    {
                        await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user)); // kullanıcının bütün rollerini siler
                        if (model.SelectedRoles != null) // EditViewModel üzerinden dönen SelectedRoles eğer null değilse seçilen rolleri user ile ilişkilendirir
                        {
                            await _userManager.AddToRolesAsync(user, model.SelectedRoles);
                        }
                        return RedirectToAction("UsersList", "Admin");
                    }
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description); // hatayı ekrana çıkarır
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("UsersList", "Admin");

        }
        public IActionResult Rollers()
        {
            return View(_roleManager.Roles);
        }
        public IActionResult RoleCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RoleCreate(AppRole model)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Rollers");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description); // hatayı ekrana çıkarır
                }

            }
            return View(model);
        }

        public async Task<IActionResult> RoleEdit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role != null && role.Name != null)
            {
                ViewBag.Users = await _userManager.GetUsersInRoleAsync(role.Name);
                return View(role);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]

        public async Task<IActionResult> RoleEdit(AppRole model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.Id);

                if (role != null)
                {
                    role.Name = model.Name;

                    var result = await _roleManager.UpdateAsync(role);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }

                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }

                    if (role.Name != null)
                        ViewBag.Users = await _userManager.GetUsersInRoleAsync(role.Name);
                }
            }

            return View(model);
        }


    }
}
