using Projet;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamrinDocs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsProduct : ContentPage
    {
        private ProductDetailsViewModel _viewModel;

        public DetailsProduct(Projet.Product selectedProduct)
        {
            InitializeComponent();
            _viewModel = new ProductDetailsViewModel(selectedProduct);
            BindingContext = _viewModel; 
        }

        private void OnPlus(object sender, EventArgs e)
        {
            _viewModel.Quantity++; 
        }

        private void OnMoins(object sender, EventArgs e)
        {
            if (_viewModel.Quantity > 1) 
                _viewModel.Quantity--; 
        }

        private async void OnAddToCartClicked(object sender, EventArgs e)
        {
            _viewModel.Product.Quantity = _viewModel.Quantity;
            CartService.AddToCart(_viewModel.Product);
            await DisplayAlert("Success", $"{_viewModel.Product.Designation} (x{_viewModel.Quantity}) has been added to your cart.", "OK");
        }
    }
}
