using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe;
using Microsoft.CodeAnalysis.CSharp.Syntax;
//using ColoradoLuxury.Models.VM;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using ColoradoLuxury.Models.VM;

namespace ColoradoLuxury.Controllers
{
    public class PaymentController : Controller
    {
        public string? SessionId { get; set; }
        private readonly IConfiguration _configuration;
        public PaymentController(IConfiguration configuration)
        {
            _configuration= configuration;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Cancel()
        {
            return View();
        }

        public ActionResult Create()
        {
            var currency = "usd";
            var successUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/Payment/Success";
            var cancelUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/Payment/Cancel";
            
            StripeConfiguration.ApiKey = _configuration.GetValue<string>("StripeSettings:Secretkey");
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                LineItems= new List<SessionLineItemOptions> {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency= currency,
                            UnitAmountDecimal = Convert.ToDecimal(HttpContext?.Session?.GetString("totalAmount")) * 100,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Travel",
                                Description = "Test"
                            }
                        },
                        Quantity = 1
                    }
                },
                Mode = "payment",
                SuccessUrl=successUrl,
                CancelUrl = cancelUrl
            };

            var service = new SessionService();
            var session = service.Create(options);
            SessionId = session.Id;

            return Redirect(session.Url);
        }
    }
}

