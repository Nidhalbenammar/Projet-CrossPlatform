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
   
        public partial class ListProductsPage : ContentPage
        {
            private readonly ProductService _productService = new ProductService();

            public ListProductsPage()
            {
                InitializeComponent();
                LoadProducts();
            }

           
            private async void LoadProducts()
            {
                var products = await _productService.GetProductsAsync();
                productsListView.ItemsSource = products;
            }

            
            private async void OnProductSelected(object sender, SelectedItemChangedEventArgs e)
            {
                var selectedProduct = e.SelectedItem as Product;
                if (selectedProduct != null)
                {
                    //normalement chnrj3lha feha shwy logique
                }
            }

           
            private async void OnEditButtonClicked(object sender, EventArgs e)
            {
                var button = sender as Button;
                var productId = (long)button.CommandParameter;
                await Navigation.PushAsync(new EditProductPage(productId));
            }

           
            private async void OnDeleteButtonClicked(object sender, EventArgs e)
            {
                var button = sender as Button;
                var productId = (long)button.CommandParameter;

                var confirmDelete = await DisplayAlert("Confirm Delete", "Are you sure you want to delete this product?", "Yes", "No");
                if (confirmDelete)
                {
                    var isDeleted = await _productService.DeleteProductAsync(productId);
                    if (isDeleted)
                    {
                        await DisplayAlert("Success", "Product deleted successfully", "OK");
                        LoadProducts();  
                    }
                    else
                    {
                        await DisplayAlert("Error", "Failed to delete product", "OK");
                    }
                }
            }
        }
    }
