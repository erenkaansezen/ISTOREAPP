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
        public OrderService _orderService;
        public Cart cart;
        private readonly UserManager<AppUser> _userManager;


        public OrderController(Cart cartService, UserManager<AppUser> userManager, StoreContext storeContext, OrderService orderService)
        {
            cart = cartService;
            _userManager = userManager;
            _storeContext = storeContext;
            _orderService = orderService;
        }

        public IActionResult Checkout()
        {
            var user = _userManager.GetUserAsync(User).Result; // Oturum açmış kullanıcıyı alıyoruz.

            var model = new OrderModel()
            {
                Cart = cart,

                User = user
                // Kullanıcı bilgisini modelde set ediyoruz.
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CheckoutAsync(OrderModel model)
        {
            var user = _userManager.GetUserAsync(User).Result; // Oturum açmış kullanıcıyı alıyoruz.


            if (cart.Items.Count == 0)
            {
                ModelState.AddModelError("", "Sepetinizde ürün yok!");            
            }
            if (ModelState.IsValid)
            {
                var order = new Order
                {
                    Name = user.FullName,
                    Email = user.Email,
                    City = model.City,
                    Phone = user.PhoneNumber ?? model.PhoneNumber,
                    AddressLine = model.AdressLine,
                    OrderDate = DateTime.Now,
                    OrderItems = cart.Items.Select(i => new ISTOREAPP.Data.Entities.OrderItem
                    {
                        ProductId = (int)i.Product.Id,
                        Price = (double)i.Product.Price,
                        Quantity = i.Quantity
                    }).ToList()
                };

                model.Cart = cart;
                var payment = await ProcessPayment(model);
                if (payment.Status=="success")
                {

                    // Order'ı Orders tablosuna ekliyoruz
                    await _orderService.AddOrderAsync(order);  // `Orders` tablosuna ekliyoruz
                    cart.Clear();
                    return RedirectToPage("/Completed", new { OrderId = order.Id });

                }
                model.Cart = cart;
                return View(model);
            }
            else
            {
                model.Cart = cart;
                return View(model);


            }

        }

        private async Task<Payment> ProcessPayment(OrderModel model)
        {
            Options options = new Options();
            options.ApiKey = "sandbox-k9ARonYWgD99rlPYRe2lXt8AwiY73qQL";
            options.SecretKey = "sandbox-RfUsS45lptzYt0gcmcl3FfpNJeaHR1du";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = new Random().Next(111111111, 999999999).ToString();
            request.Price = model?.Cart?.CalculateTotal().ToString();
            request.PaidPrice = model?.Cart?.CalculateTotal().ToString();
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = "B67832";
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = model?.CartName;
            paymentCard.CardNumber = model?.CartNumber;
            paymentCard.ExpireMonth = model?.ExpirationMonth;
            paymentCard.ExpireYear = model?.ExpirationYear;
            paymentCard.Cvc = model?.Cvc;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            Buyer buyer = new Buyer();
            buyer.Id = "BY789";
            buyer.Name = model?.CartName;
            buyer.Surname = model?.CartSurname;
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

            foreach (var item in model?.Cart?.Items ?? Enumerable.Empty<CartItem>())
            {
                BasketItem firstBasketItem = new BasketItem();
                firstBasketItem.Id = item.Product.Id.ToString();
                firstBasketItem.Name = item.Product.Name;
                firstBasketItem.Category1 = "Telefon";
                firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
                firstBasketItem.Price = item.Product.Price.ToString();
                basketItems.Add(firstBasketItem);
            }


            request.BasketItems = basketItems;

            Iyzipay.Model.Payment payment = await Payment.Create(request, options);
            return payment;
        }
    }
}
