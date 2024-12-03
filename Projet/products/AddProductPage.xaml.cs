using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Projet.products
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddProductPage : ContentPage
    {
        private readonly ProductService _productService = new ProductService();

        public AddProductPage()
        {
            InitializeComponent();
            LoadCategories();
        }

       
        private async void LoadCategories()
        {
            var categories = await _productService.GetCategoriesAsync();
            categoryPicker.ItemsSource = categories;
        }

        
        private async void OnAddProductClicked(object sender, EventArgs e)
        {
            var designation = designationEntry.Text;
            var photo = photoEntry.Text;
            var price = double.TryParse(priceEntry.Text, out double result) ? result : 0;

            var selectedCategory = categoryPicker.SelectedItem as Category;
            if (selectedCategory == null)
            {
                await DisplayAlert("Error", "Please select a category", "OK");
                return;
            }

            var categoryId = selectedCategory.Id;

            var isSuccess = await _productService.AddProductAsync(designation, photo, price, categoryId);

            if (isSuccess)
            {
                await DisplayAlert("Success", "Product added successfully", "OK");
                await Navigation.PopAsync();  
            }
            else
            {
                await DisplayAlert("Error", "Failed to add product", "OK");
            }
        }
    }
}