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
    public partial class SignupPage : ContentPage
    {
        private readonly AuthService _authService;

        public SignupPage()
        {
            InitializeComponent();
            _authService = new AuthService();
        }

        private async void OnSignupClicked(object sender, EventArgs e)
        {
            if (PasswordEntry.Text != ConfirmPasswordEntry.Text)
            {
                await DisplayAlert("Error", "Password and Confirm Password must be the same.", "OK");
                return; 
            }
            var success = await _authService.Signup(
                NomEntry.Text,
                PrenomEntry.Text,
                UsernameEntry.Text,
                PasswordEntry.Text);

            if (success)
            {
                await DisplayAlert("Success", "Welcome please log in !", "OK");
                await Navigation.PopAsync(); 
            }
            else
            {
                await DisplayAlert("Error", "Signup failed", "OK");
            }
        }
    }
}