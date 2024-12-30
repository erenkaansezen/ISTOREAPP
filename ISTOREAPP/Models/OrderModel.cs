using ISTOREAPP.Data.Entities;

namespace ISTOREAPP.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public AppUser? User { get; set; }

        public string City { get; set; }
        public string AdressLine { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new();
        public Cart Cart { get; set; } = new();
    }
}
