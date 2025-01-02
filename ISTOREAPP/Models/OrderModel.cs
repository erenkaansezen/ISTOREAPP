using ISTOREAPP.Data.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace ISTOREAPP.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        public AppUser? User { get; set; }

        [Display(Name = "Şehir Adı")]
        public string? City { get; set; }
        [Display(Name = "Gönderim Adresi")]
        public string? AdressLine { get; set; }
        [BindNever]
        public Cart Cart { get; set; } = new();

        [Display(Name = "Kartın Üstündeki Kullanıcı Adı")]
        [Required(ErrorMessage = "Sepet adı gereklidir.")]
        public string? CartName { get; set; }

        [Display(Name = "Kart Numarası")]
        [Required(ErrorMessage = "Kart numarası gereklidir.")]
        [CreditCard(ErrorMessage = "Geçerli bir kart numarası girin.")]
        public string? CartNumber { get; set; } 

        [Display(Name = "Son Kullanma Yılı")]
        [Required(ErrorMessage = "Son kullanma yılı gereklidir.")]
        [Range(2024, 2099, ErrorMessage = "Geçerli bir yıl girin.")]
        public string? ExpirationYear { get; set; }

        [Display(Name = "Son Kullanma Ayı")]
        [Required(ErrorMessage = "Son kullanma ayı gereklidir.")]
        [Range(1, 12, ErrorMessage = "Geçerli bir ay girin.")]
        public string? ExpirationMonth { get; set; }

        [Display(Name = "CVC")]
        [Required(ErrorMessage = "CVC gereklidir.")]
        [RegularExpression(@"^\d{3,4}$", ErrorMessage = "Geçerli bir CVC numarası girin.")]
        public string? Cvc { get; set; }


    }
}
