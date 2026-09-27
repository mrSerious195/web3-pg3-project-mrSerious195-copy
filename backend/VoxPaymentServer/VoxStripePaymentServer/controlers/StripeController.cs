using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using VoxStripePaymentServer.models;
using Stripe.Checkout;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using VoxelGallery.Api.Contracts;
using Duende.IdentityModel.Client;
using Microsoft.IdentityModel.Tokens;
namespace VoxStripePaymentServer.controlers
{
    [Route("stripe")]
    [ApiController]
    public class StripeController(
        IOptions<StripeOptions> options,
        ProductService productService,
        IHttpClientFactory httpClientFactory,
        IConfiguration config
    ) : ControllerBase
    {

        public class payrequestcontract
        {
            public string priceId { get; set; }
        }

        [Authorize(Policy = "User")]
        [HttpPost("")]
        public ActionResult Pay([FromBody] payrequestcontract price)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            StripeConfiguration.ApiKey = options.Value.ApiKey;
            var sessionCreateOptions = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
              {
                  new SessionLineItemOptions
                  {
                      Price=price.priceId,
                      Quantity=1
                  }
              },

                Metadata = new Dictionary<string, string>
                {
                    ["userId"] = userId,
                },

                Mode = "payment",
                SuccessUrl =options.Value.SuccessUrl,
                CancelUrl = options.Value.CancelUrl,
            };
            var service = new SessionService();
            Session session = service.Create(sessionCreateOptions);
            return Ok(session.Url);
        }

        


        [HttpGet("products")]
         [Authorize(Policy ="User")]
        public async Task<dynamic> GetAllProducts()
        {
            StripeConfiguration.ApiKey = options.Value.ApiKey;
            var productListOptions = new ProductListOptions { Expand = new List<string>() { "data.default_price" } };
            var products = productService.List(productListOptions);
            return new { products };
        }

        public class MetadataStripeServerResponseContract
        {
            public string userId { get; set; }
        }

        public class StripeServerResponseContract
        {
            public MetadataStripeServerResponseContract metadata { get; set; }
        }




        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
            using (StreamReader sr = new StreamReader(HttpContext.Request.Body))
            {
               var json = await sr.ReadToEndAsync();
                if (json == null)
                {
                    throw new Exception("can't parse HttpContext.Request.Body");
                }

    Stripe.Event stripeEvent;
      try
    {
        stripeEvent = EventUtility.ConstructEvent(
            json,
            Request.Headers["Stripe-Signature"],
            options.Value.Secretkey
        );
    }
    catch (StripeException)
    {
        return Unauthorized();
    }

                
                   
                if (stripeEvent.Type == "checkout.session.completed")
                {
                    var session = stripeEvent.Data.Object as Stripe.Checkout.Session;

                    var userId = session.Metadata["userId"];
                  var data =  new DonationRequestContract
                    {
                        CustomerId=userId
                    };
                      var IdClient = httpClientFactory.CreateClient("IdentityServer");
                    var VoxClient = httpClientFactory.CreateClient("VoxData");

var disco = await IdClient.GetDiscoveryDocumentAsync();
var tokenResponse = await IdClient.RequestClientCredentialsTokenAsync(
            new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "m2m-client",
                ClientSecret = config["m2msecret"],
                Scope = "VoxelGallery.Api.m2m"
            });
        VoxClient.SetBearerToken(tokenResponse.AccessToken ?? throw new Exception("Could not obtain token."));
                    
                    await VoxClient.PostAsJsonAsync("api/donations",data);
                }

                return Ok();
            }
        }

    }
}
