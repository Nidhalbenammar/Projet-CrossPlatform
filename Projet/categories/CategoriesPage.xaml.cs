using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Projet.categories
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoriesPage : ContentPage
    {
        private readonly ApiService _apiService = new ApiService();

        public CategoriesPage()
        {
            InitializeComponent();
            LoadCategories();
        }

        
        private async void LoadCategories()
        {
            var categories = await _apiService.GetCategoriesAsync();
            CategoriesListView.ItemsSource = categories;
        }


        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var category = button?.BindingContext as Category;

            if (category != null)
            {
            
                bool confirmDelete = await DisplayAlert(
                    "Confirm Deletion",
                    $"Are you sure you want to delete the category '{category.Name}'?",
                    "Yes",
                    "No"
                );

                if (!confirmDelete)
                {
                    return;
                }

                var products = await _apiService.GetProductsByCategoryAsync(category.Id);

                if (products.Any()) 
                {
                    
                    await DisplayAlert(
                        "Cannot Delete",
                        "You need to delete the products in this category before deleting the category.",
                        "OK"
                    );
                }
                else
                {
                    bool isDeleted = await _apiService.DeleteCategoryAsync(category.Id);
                    if (isDeleted)
                    {
                        await DisplayAlert("Success", "Category deleted successfully", "OK");
                        LoadCategories(); 
                    }
                    else
                    {
                        await DisplayAlert("Error", "Failed to delete category", "OK");
                    }
                }
            }
        }



    }
}