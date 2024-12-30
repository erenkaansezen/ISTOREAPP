using ISTOREAPP.Data.Entities;

namespace ISTOREAPP.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public void AddItem(Product product, int quantity)
        {
            var item = Items.Where(p => p.Product.Id == product.Id).FirstOrDefault();
            // sepette bir ürün varmı kontrol eder
            if (item == null)
            {
                //Eğer sepette bir ürün yoksa sepete seçilen ürünü ekler
                Items.Add(new CartItem { Product = product, Quantity = quantity });
            }
            else
            {
                //eğer sepette seçilen ürün varsa adet sayısını arttırır
                item.Quantity += quantity;
            }
        }
        public void ItemDecrease(Product product, int quantity)
        {
            var item = Items.Where(p => p.Product.Id == product.Id).FirstOrDefault();
            // sepette bir ürün varmı kontrol eder
            if (item != null)
            {
                //Eğer sepette bir ürün yoksa sepete seçilen ürünü ekler
                item.Quantity -= 1;
                if (item.Quantity < 1)
                {
                    Items.Remove(item);  // Sepetten çıkar
                }
            }

        }
        public void RemoveItem(Product product)
        {
            //seçilen ürünü sepetten kaldırır
            Items.RemoveAll(i => i.Product.Id == product.Id);
        }
        public decimal CalculateTotal()
        {
            //seçilen ürünün product üzerindeki fiyatı ile adet fiyatını çarpar ve toplam tutarı gösterir
            return (decimal)Items.Sum(i => i.Product.Price * i.Quantity);
        }

        public void Clear()
        {
            //Sepeti komple temizler
            Items.Clear();
        }

        public static implicit operator Cart(List<CartItem> v)
        {
            throw new NotImplementedException();
        }
    }
    public class CartItem
    {
        public int CartItemId { get; set; }
        public Product Product { get; set; } = new();
        public int Quantity { get; set; }
    }
}
