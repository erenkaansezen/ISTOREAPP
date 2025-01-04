using ISTOREAPP.Data.Entities;

namespace ISTOREAPP.ViewModels
{
    public class OrderViewModel
    {
        public IEnumerable<Order> Orders { get; set; }

        public IEnumerable<OrderItem> OrderItems { get; set; }

    }
}
