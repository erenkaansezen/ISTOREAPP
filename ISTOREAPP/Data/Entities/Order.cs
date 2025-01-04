namespace ISTOREAPP.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string Name { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string AddressLine { get; set; } = null!;
        public AppUser? User { get; set; }

        public bool approve { get; set; } = false;
        public double TotalAmount => OrderItems.Sum(item => item.Price * item.Quantity);

        public List<OrderItem> OrderItems { get; set; } = new();
    }
    public class OrderItem
        {
            public int Id { get; set; }
            public int OrderId { get; set; }
            public Order Order { get; set; } = new();

            public int ProductId { get; set; }
            public Product Product { get; set; }
            public double Price { get; set; }
            public int Quantity { get; set; }
        }
   
}
