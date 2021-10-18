using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Entites
{
    public class ShoppingCart
    {
        public string UserName { get; set; }
        public List<ShoppingCartItems> Items { get; set; } = new List<ShoppingCartItems>();

        public ShoppingCart()
        {

        }

        public ShoppingCart(string userName )
        {
            UserName = userName;
        }

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                foreach (var item in Items)
                {
                    totalPrice += item.Price * item.Quentity;
                }
                return totalPrice;
            }
        }
    }
}
