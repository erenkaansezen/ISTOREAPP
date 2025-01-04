using ISTOREAPP.Data.Context;
using ISTOREAPP.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class OrderService
    {
        private readonly StoreContext _context;

        public OrderService(StoreContext context)
        {
            _context = context;
        }


        /// Tüm Satışları getirir.

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderItems) // OrderItems ile ilişkiyi dahil ediyoruz
                .ThenInclude(oi => oi.Product) // Eğer ürün detaylarını da getirecekseniz, Product'ı dahil edebilirsiniz
                .ToListAsync();
        }


        /// Belirli bir ID'ye sahip Satışı getirir.
        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        /// Yeni bir satış ekler.
        public async Task AddCategoryAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        /// Mevcut bir satışı günceller.
        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        /// Belirli bir ID'ye sahip satışı siler.
        public async Task DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
        public async Task Approve(int id)
        {
            var order = _context.Orders.FirstOrDefault(s => s.Id == id);


            order.approve = !order.approve; // Durumu tersine çevir
            await _context.SaveChangesAsync();
        }
    }
}
