using InternshipTradingApp.AccountManagement.DTOs;
using InternshipTradingApp.AccountManagement.Entities;
using InternshipTradingApp.AccountManagement.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace InternshipTradingApp.Server.Controllers.AccountManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeController(UserManager<AppUser> userManager, IFundsService fundsService) : ControllerBase
    {
        [HttpPost("create-checkout-session")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] AddFundsDto request)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = request.Amount * 100,
                            Currency = "ron",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Deposit funds"
                            },
                        },
                        Quantity = 1,
                    },
                },
                Mode = "payment",
                //SuccessUrl = $"http://localhost:4200/success?amount={request.Amount}&userId={user.Id}&username={Uri.EscapeDataString(user.UserName!)}",
                SuccessUrl = $"http://localhost:4200/success",
                CancelUrl = "http://localhost:4200/cancel",
                Metadata = new Dictionary<string, string>
                {
                    { "UserId", user.Id.ToString() }
                }
            };

            var service = new SessionService();
            Session session = await service.CreateAsync(options);

            return Ok(new { url = session.Url });
        }



        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
            const string endpointSecret = "whsec_bc9ca4f1726b53a4a44d353e1242454844f4cb33c170018b9ebd0f9e24b1c0a3";

            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var signatureHeader = Request.Headers["Stripe-Signature"].ToString();

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json, signatureHeader, endpointSecret);

                switch (stripeEvent.Type)
                {
                    case Events.PaymentIntentSucceeded:
                        break;

                    case Events.CheckoutSessionCompleted:
                        var checkoutSession = stripeEvent.Data.Object as Session;
                        if (checkoutSession != null)
                        {
                            var userId = checkoutSession.Metadata["UserId"];

                            if (string.IsNullOrEmpty(userId) || checkoutSession.AmountTotal == null || checkoutSession.AmountTotal.Value <= 0)
                                return BadRequest();

                            var amountInDollars = checkoutSession.AmountTotal.Value / 100.0m;

                            await fundsService.AddFundsAsync(int.Parse(userId), amountInDollars);
                        }
                        break;



                    default:
                        Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                        break;
                }

                return Ok();
            }
            catch (StripeException e)
            {
                Console.WriteLine("Stripe error: {0}", e.Message);
                return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine("General error: {0}", e.Message);
                return StatusCode(500);
            }
        }

    }
}
