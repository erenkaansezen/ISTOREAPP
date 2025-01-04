using ISTOREAPP.Data.Entities;
using ISTOREAPP.Helpers;
using ISTOREAPP.Models;
using System.Text.Json.Serialization;

namespace ISTOREAPP.Business.Services
{
    public class SessionCart : Cart
    {
        public static Cart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>().HttpContext?.Session;
            SessionCart cart = session?.GetJson<SessionCart>("Cart") ?? new SessionCart();
            cart.Session = session;
            return cart;
        }



        [JsonIgnore]
        public ISession? Session { get; set; }
        public override void AddItem(Product product, int quantity)
        {
            base.AddItem(product, quantity);
            Session?.SetJson("Cart", this);
        }

        public override void RemoveItem(Product product)
        {
            base.RemoveItem(product);
            Session?.SetJson("Cart", this);

        }

        public override void Clear()
        {
            base.Clear();
            Session?.SetJson("Cart", this);

        }
    }
}
