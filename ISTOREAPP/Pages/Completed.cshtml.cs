using ISTOREAPP.Data.Context;
using ISTOREAPP.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ISTOREAPP.Pages
{
    public class CompletedModel : PageModel
    {
        public StoreContext _storeContext;
        public CompletedModel(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        [BindProperty(SupportsGet = true)]
        public int OrderId { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var order = await _storeContext.Orders
                .Include(o => o.OrderItems) // Order ile iliþkili OrderItem'larý dahil ediyoruz
                .FirstOrDefaultAsync(o => o.Id == OrderId);
            
            return Page();
        }
    }
}
