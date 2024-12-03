using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet
{
    public static class CartService
    {
        private static readonly List<Product> Cart = new List<Product>();

        public static void AddToCart(Product product)
        {
            Cart.Add(product);
        }

        public static List<Product> GetCartItems()
        {
            return new List<Product>(Cart);
        }
        public static void RemoveFromCart(Product product)
        {
            var productToRemove = Cart.FirstOrDefault(p => p.Id == product.Id);
            if (productToRemove != null)
            {
                Cart.Remove(productToRemove);
            }
        }


        public static void ClearCart()
        {
            Cart.Clear();
        }
        public static decimal GetTotalPrice()
        {
            return (decimal)Cart.Sum(product => product.Price * product.Quantity);
        }

    }
}
