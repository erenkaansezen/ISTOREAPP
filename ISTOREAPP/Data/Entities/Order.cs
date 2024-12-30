namespace ISTOREAPP.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        public string UserId { get; set; }
        public AppUser? User { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new();
    }
    public class OrderItem
        {
            public int Id { get; set; }
            public int OrderId { get; set; }
            public Order Order { get; set; } = new();

            public int ProductId { get; set; }
            public Product Product { get; set; } = new();
            public double Price { get; set; }
            public int Quantity { get; set; }
        }
   
}
