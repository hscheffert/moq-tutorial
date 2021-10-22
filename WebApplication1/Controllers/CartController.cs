using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        //private readonly ILogger<CartController> _logger;
        private readonly ICartService _cartService;
        private readonly IPaymentService _paymentService;
        private readonly IShipmentService _shipmentService;

        public CartController(//ILogger<CartController> logger,
            ICartService cartService,
            IPaymentService paymentService,
            IShipmentService shipmentService)
        {
            //_logger = logger;
            _cartService = cartService;
            _paymentService = paymentService;
            _shipmentService = shipmentService;
        }

        [HttpPost]
        public string CheckOut(ICard card, IAddressInfo addressInfo)
        {
            var result = _paymentService.Charge(_cartService.Total(), card);

            if (result)
            {
                _shipmentService.Ship(addressInfo, _cartService.Items());
                return "charged";
            }

            return "not charged";
        }
    }
}
