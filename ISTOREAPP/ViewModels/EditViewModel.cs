using System.ComponentModel.DataAnnotations;

namespace ISTOREAPP.ViewModels
{
    public class EditViewModel
    {
        public string? Id { get; set; }
        [Display(Name = "Kullanıcı Adı")]

        public string? FullName { get; set; }

        [EmailAddress]
        [Display(Name = "Email Adresi ")]

        public string? Email { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Kullanıcı Şifresi")]

        public string? Password { get; set; } = string.Empty;
        
        [DataType(DataType.Password)] // data tipi belirtilir
        [Compare(nameof(Password),ErrorMessage ="Parola Eşleşmiyor")] // hata mesajı çıkarır
        [Display(Name = "Şifrenizi  Doğrulayınız")]

        public string? ConfirmPassword { get; set; } = string.Empty;

    }
}
