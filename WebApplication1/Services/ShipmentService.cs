using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IShipmentService
    {
        void Ship(IAddressInfo info, IEnumerable<CartItem> items);
    }
    //public class ShipmentService : IShipmentService
    //{
    //}
}
