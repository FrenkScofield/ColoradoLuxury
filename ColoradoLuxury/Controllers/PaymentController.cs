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
        public string SessionId { get; set; }
        
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
            var successUrl = "https://192.168.0.106:5000/Payment/Success";
            var cancelUrl = "https://192.168.0.106:5000/Payment/Cancel";

            StripeConfiguration.ApiKey = "sk_test_51DxVxFCfsgGnUgqjqZkYUOm9ZCixkzcRloQSlTYsEE3zRAbb18JJFRRPC99g1MY5qxW5dgvkrLIYxfI056zutLX000ft9sfaQV";
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
                            UnitAmount = Convert.ToInt32("50") * 100,
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

