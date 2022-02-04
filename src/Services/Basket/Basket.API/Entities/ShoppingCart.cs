﻿namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; }
        public List<ShoppingCartItem> items { get; set; } = new List<ShoppingCartItem>();
        public ShoppingCart()
        {

        }
        public ShoppingCart(string userName)
        {
            UserName = userName;
        }
        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                foreach (ShoppingCartItem item in items)
                {
                    totalPrice += item.Price * item.Quantity;
                }
                return totalPrice;
            }
        }
    }
}
