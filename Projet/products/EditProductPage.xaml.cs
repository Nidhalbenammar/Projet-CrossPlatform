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
    public partial class EditProductPage : ContentPage
    {
        private readonly ProductService _productService = new ProductService();
        private readonly long _productId;

        public EditProductPage(long productId)
        {
            InitializeComponent();
            _productId = productId;
            LoadProduct();
            LoadCategories();
        }

       
        private async void LoadProduct()
        {
            var product = await _productService.GetProductByIdAsync(_productId);
            if (product != null)
            {
                designationEntry.Text = product.Designation;
                photoEntry.Text = product.Photo;
                priceEntry.Text = product.Price.ToString();
                var categories = await _productService.GetCategoriesAsync();
                categoryPicker.ItemsSource = categories;

               
                var selectedCategory = categories.FirstOrDefault(c => c.Id == product.CategoryId);
                if (selectedCategory != null)
                {
                    categoryPicker.SelectedItem = selectedCategory;
                }
            }
        }

        
        private async void LoadCategories()
        {
            var categories = await _productService.GetCategoriesAsync();
            categoryPicker.ItemsSource = categories;
        }

       
        private async void OnUpdateButtonClicked(object sender, EventArgs e)
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

            var isUpdated = await _productService.UpdateProductAsync(_productId, designation, photo, price, categoryId);

            if (isUpdated)
            {
                await DisplayAlert("Success", "Product updated successfully", "OK");
                await Navigation.PopAsync();  
            }
            else
            {
                await DisplayAlert("Error", "Failed to update product", "OK");
            }
        }
    }
}