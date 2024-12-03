using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamrinDocs;

namespace Projet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeAdminPage : FlyoutPage
    {
        private ApiService _apiService;

        public HomeAdminPage()
        {
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
        private async void OnAddCategoryClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new categories.AddCategoryPage());
        }
        private async void OnManageCategoryClicked(object sender, EventArgs e)
        {
            
            await Navigation.PushAsync(new categories.CategoriesPage());
        }
        private async void OnAddProductClicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new products.AddProductPage());
        }
        private async void OnManageProductClicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new products.ListProductsPage());
        }
        private async void OnAddUserClicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new users.AddUserPage());
        }
        private async void OnManageUserClicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new users.ListUsersPage());
        }
        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            SessionManager.EndSession();
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }


    }
}