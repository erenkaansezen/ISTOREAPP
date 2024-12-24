using ISTOREAPP.Data.Entities;
using Microsoft.AspNetCore.Identity;


namespace Business.Services
{
    public class UserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UserService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        /// Tüm kullanıcıları getirir.

        public async Task<IEnumerable<AppUser>> GetAllUsersAsync()
        {
            return _userManager.Users.ToList();
        }


        /// Belirli bir ID'ye sahip kullanıcıyı getirir.

        public async Task<AppUser?> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }


        /// Yeni bir kullanıcı oluşturur.

        public async Task<IdentityResult> CreateUserAsync(AppUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }


        /// Kullanıcıyı günceller.

        public async Task<IdentityResult> UpdateUserAsync(AppUser user)
        {
            return await _userManager.UpdateAsync(user);
        }


        /// Belirli bir kullanıcıyı siler.

        public async Task<IdentityResult> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                return await _userManager.DeleteAsync(user);
            }
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });
        }


        /// Kullanıcıya bir rol atar.

        public async Task<IdentityResult> AddUserToRoleAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                return await _userManager.AddToRoleAsync(user, role);
            }
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });
        }


        /// Kullanıcının rollerini getirir.

        public async Task<IList<string>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                return await _userManager.GetRolesAsync(user);
            }
            return new List<string>();
        }


        /// Kullanıcıyı belirli bir rolden çıkarır.

        public async Task<IdentityResult> RemoveUserFromRoleAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                return await _userManager.RemoveFromRoleAsync(user, role);
            }
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });
        }
    }
}
