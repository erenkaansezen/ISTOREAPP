using Microsoft.AspNetCore.Identity;

namespace ISTOREAPP.Data.Entities
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; } = string.Empty;

    }
}
