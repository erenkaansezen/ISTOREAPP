using ISTOREAPP.Data.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ISTOREAPP.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public AppUser? User { get; set; }

        public string City { get; set; }
        public string AdressLine { get; set; }
        [BindNever]
        public Cart Cart { get; set; } = new();
    }
}
