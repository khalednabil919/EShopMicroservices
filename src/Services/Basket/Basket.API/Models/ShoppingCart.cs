﻿using System.Runtime.CompilerServices;

namespace Basket.API.Models
{
    public class ShoppingCart
    {
        public string UserName { get; set; } = default!;
        public List<ShoppingCartItem> Items { get; set; } = new();
        public decimal TotalPrice => Items.Sum(x => x.Quantity * x.Price);
        public ShoppingCart(string UserName)
        {
            this.UserName = UserName;
        }
        public ShoppingCart()
        {
            
        }
    }
}
