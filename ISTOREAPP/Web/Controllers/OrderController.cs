using Business.Services;

using ISTOREAPP.Data.Context;
using ISTOREAPP.Data.Entities;
using ISTOREAPP.Helpers;
using ISTOREAPP.Models;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Options = Iyzipay.Options;

namespace ISTOREAPP.Web.Controllers
{
    public class OrderController:Controller
    {
        public StoreContext _storeContext;

        public Cart _cart;
        private readonly UserManager<AppUser> _userManager;


        public OrderController(Cart cartService, UserManager<AppUser> userManager, StoreContext storeContext)
        {
            _cart = cartService;
            _userManager = userManager;
            _storeContext = storeContext;
        }

        public IActionResult Checkout()
        {
            var user = _userManager.GetUserAsync(User).Result; // Oturum açmış kullanıcıyı alıyoruz.
            var carted = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();

            var model = new OrderModel()
            {
                Cart = carted,

                User = user
                // Kullanıcı bilgisini modelde set ediyoruz.
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CheckoutAsync(OrderModel model)
        {
            var user = _userManager.GetUserAsync(User).Result; // Oturum açmış kullanıcıyı alıyoruz.

            var carted = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();

            if (carted.Items.Count == 0)
            {
                ModelState.AddModelError("", "Sepetinizde ürün yok!");            
            }
            if (ModelState.IsValid)
            {
                var order = new Order
                {
                    Name = user.UserName,
                    Email = user.Email,
                    City = model.City,
                    Phone = user.PhoneNumber,
                    AddressLine = model.AdressLine,
                    OrderDate = DateTime.Now,
                    OrderItems = carted.Items.Select(i => new ISTOREAPP.Data.Entities.OrderItem
                    {
                        ProductId = (int)i.Product.Id,
                        Price = (double)i.Product.Price,
                        Quantity = i.Quantity
                    }).ToList()
                };

                var payment = await ProcessPayment();
                if (payment.Status=="success")
                {

                    // Order'ı Orders tablosuna ekliyoruz
                    await _storeContext.Orders.AddAsync(order);  // `Orders` tablosuna ekliyoruz

                    // Veritabanına kaydediyoruz
                    await _storeContext.SaveChangesAsync();  // Veritabanına işlemi kaydediyoruz

                    // Sepeti temizleyebiliriz (isteğe bağlı)
                    HttpContext.Session.Remove("cart");
                    return RedirectToPage("/Completed", new { OrderId = order.Id });

                }

                return RedirectToPage("/Completed", new { OrderId = order.Id });
            }
            else
            {
                return RedirectToPage("/StorePage");

            }

        }

        private async Task<Payment> ProcessPayment()
        {
            Options options = new Options();
            options.ApiKey = "sandbox-k9ARonYWgD99rlPYRe2lXt8AwiY73qQL";
            options.SecretKey = "sandbox-RfUsS45lptzYt0gcmcl3FfpNJeaHR1du";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = "123456789";
            request.Price = "1";
            request.PaidPrice = "1.2";
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = "B67832";
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = "John Doe";
            paymentCard.CardNumber = "5528790000000008";
            paymentCard.ExpireMonth = "12";
            paymentCard.ExpireYear = "2030";
            paymentCard.Cvc = "123";
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            Buyer buyer = new Buyer();
            buyer.Id = "BY789";
            buyer.Name = "John";
            buyer.Surname = "Doe";
            buyer.GsmNumber = "+905350000000";
            buyer.Email = "email@email.com";
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            buyer.Ip = "85.34.78.112";
            buyer.City = "Istanbul";
            buyer.Country = "Turkey";
            buyer.ZipCode = "34732";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = "Jane Doe";
            shippingAddress.City = "Istanbul";
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = "Jane Doe";
            billingAddress.City = "Istanbul";
            billingAddress.Country = "Turkey";
            billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem firstBasketItem = new BasketItem();
            firstBasketItem.Id = "BI101";
            firstBasketItem.Name = "Binocular";
            firstBasketItem.Category1 = "Collectibles";
            firstBasketItem.Category2 = "Accessories";
            firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            firstBasketItem.Price = "0.3";
            basketItems.Add(firstBasketItem);

            BasketItem secondBasketItem = new BasketItem();
            secondBasketItem.Id = "BI102";
            secondBasketItem.Name = "Game code";
            secondBasketItem.Category1 = "Game";
            secondBasketItem.Category2 = "Online Game Items";
            secondBasketItem.ItemType = BasketItemType.VIRTUAL.ToString();
            secondBasketItem.Price = "0.5";
            basketItems.Add(secondBasketItem);

            BasketItem thirdBasketItem = new BasketItem();
            thirdBasketItem.Id = "BI103";
            thirdBasketItem.Name = "Usb";
            thirdBasketItem.Category1 = "Electronics";
            thirdBasketItem.Category2 = "Usb / Cable";
            thirdBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            thirdBasketItem.Price = "0.2";
            basketItems.Add(thirdBasketItem);
            request.BasketItems = basketItems;

            Iyzipay.Model.Payment payment = await Payment.Create(request, options);
            return payment;
        }
    }
}
