using Projet;
using Projet.products;
using Projet.users;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamrinDocs
{
    public partial class HomePage : ContentPage
    {
        private ApiService _apiService;

        public HomePage()

        {
            if (!SessionManager.IsLoggedIn)
            {
                
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                return;
            }
            InitializeComponent();
            _apiService = new ApiService();
            LoadCategories();
            
        }

        private async void LoadCategories()
        {
            var categories = await _apiService.GetCategoriesAsync();
            foreach (var category in categories)
            {
                var products = await _apiService.GetProductsByCategoryAsync(category.Id);
                category.Products = products;
            }
            CategoriesListView.ItemsSource = categories;
        }
        private async void LoadCategories2(object sender, EventArgs e)
        {
            var categories = await _apiService.GetCategoriesAsync();
            foreach (var category in categories)
            {
                var products = await _apiService.GetProductsByCategoryAsync(category.Id);
                category.Products = products;
            }
            CategoriesListView.ItemsSource = categories;
        }
        private async void OnProductTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                
                var selectedProduct = e.Item as Product;
                await Navigation.PushAsync(new DetailsProduct(selectedProduct));
            }
        }
        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            SessionManager.EndSession();
            CartService.ClearCart();
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
        private async void OnSearchClicked(object sender, EventArgs e)
        {
            
            var searchPopup = new SearchPopup();
            searchPopup.SearchCompleted += async (searchQuery) =>
            {
                if (!string.IsNullOrEmpty(searchQuery))
                {
                    
                    var products = await _apiService.GetProductsByDesignationAsync(searchQuery);
                    CategoriesListView.ItemsSource = new List<Category>
            {
                new Category
                {
                    Name = $"Search Results for '{searchQuery}'",
                    Products = products
                }
            };
                }
            };

            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(searchPopup);
        }
        private async void OnCartClicked(object sender, EventArgs e)
        {
           
            await Navigation.PushAsync(new CartPage());
        }
        private async void OnProfileClicked(object sender, EventArgs e)
        {
            var currentUser = await _apiService.GetUserByUsernameAsync(SessionManager.Username);
            await Navigation.PushAsync(new UserProfile(currentUser));
        }



    }
}
