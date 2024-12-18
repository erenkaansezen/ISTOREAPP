using System.ComponentModel.DataAnnotations;

namespace ISTOREAPP.ViewModels
{
    public class CreateViewModel
    {
        [Required] // zorunlu olmasını ister
        public required string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [Display(Name = "Email Adresi ")]

        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Kullanıcı Şifresi")]

        public string Password { get; set; } = string.Empty;
        
        [Required]
        [DataType(DataType.Password)] // data tipi belirtilir
        [Compare(nameof(Password),ErrorMessage ="Parola Eşleşmiyor")] // hata mesajı çıkarır
        [Display(Name = "Şifrenizi  Doğrulayınız")]

        public string ConfirmPassword { get; set; } = string.Empty;

    }
}
