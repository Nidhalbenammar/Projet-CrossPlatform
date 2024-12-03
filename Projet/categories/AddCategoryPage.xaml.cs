using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Projet.categories
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCategoryPage : ContentPage
    {
        private ApiService _apiService;

        public AddCategoryPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
        }

        private async void OnAddCategoryClicked(object sender, EventArgs e)
        {
            var categoryName = CategoryNameEntry.Text;
            var categoryPhoto = CategoryPhotoEntry.Text;

            if (string.IsNullOrWhiteSpace(categoryName) || string.IsNullOrWhiteSpace(categoryPhoto))
            {
                await DisplayAlert("Error", "Please fill in all fields", "OK");
                return;
            }

            var result = await _apiService.AddCategoryAsync(categoryName, categoryPhoto);

            if (result)
            {
                await DisplayAlert("Success", "Category added successfully", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Failed to add category", "OK");
            }
        }
    }
}
