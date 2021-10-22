﻿using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface ICartService
    {
        double Total();
        IEnumerable<CartItem> Items();
    }

    //public class CartService : ICartService
    //{
    //}
}
