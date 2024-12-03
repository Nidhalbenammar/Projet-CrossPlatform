using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Projet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {
        public CartPage()
        {
            InitializeComponent();
            LoadCartItems();
            UsernameLabel.Text = $"Welcome, {SessionManager.Username}";
            if (!SessionManager.IsLoggedIn)
            {
                CartService.ClearCart();
            }
        }
        
         
        private void UpdateTotalPrice()
        {
            decimal totalPrice = CartService.GetTotalPrice();
            TotalPriceLabel.Text = $"Total: {totalPrice} TND";
        }
        private void LoadCartItems()
        {
            var cartItems = CartService.GetCartItems();
            var cartItemList = cartItems.Select(p => new CartItem(p.Designation, p.Price, p.Quantity, p.Photo)).ToList();
            CartListView.ItemsSource = cartItemList;
            UpdateTotalPrice();
        }
        private void OnDeleteClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var cartItem = (CartItem)button.CommandParameter;  

          
            var product = CartService.GetCartItems()
                .FirstOrDefault(p => p.Designation == cartItem.Designation && p.Price == cartItem.Price);

            if (product != null)
            {
                CartService.RemoveFromCart(product);  
                LoadCartItems();  
            }
        }


        private void OnClearCartClicked(object sender, EventArgs e)
        {
           
            CartService.ClearCart();
            LoadCartItems();
        }
        private async void OnValidateClicked(object sender, EventArgs e)
        {
            
            var cartItems = CartService.GetCartItems();
            var cartItemList = cartItems.Select(p => new CartItem(p.Designation, p.Price, p.Quantity, p.Photo)).ToList();

            var commandeService = new CommandeService();

            bool result = await commandeService.AddCommandeAsync(SessionManager.Username, cartItemList);

            if (result)
            {
                await DisplayAlert("Congratulations", "Commande successfully purchased", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Failed to add commande", "OK");
            }
        }




    }
}
