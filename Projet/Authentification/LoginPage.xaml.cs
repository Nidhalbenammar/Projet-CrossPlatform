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
    public partial class LoginPage : ContentPage
    {
        private readonly AuthService _authService;

        public LoginPage()
        {
            InitializeComponent();
            _authService = new AuthService();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var user = await _authService.Login(UsernameEntry.Text, PasswordEntry.Text);

            if (user == null)
            {
                await DisplayAlert("Error", "Invalid username or password", "OK");
                return;
            }

            SessionManager.StartSession(user.Username);

            if (user.Role.Equals("ADMIN", StringComparison.OrdinalIgnoreCase))
            {
                await Navigation.PushAsync(new HomeAdminPage());
            }
            else if (user.Role.Equals("USER", StringComparison.OrdinalIgnoreCase))
            {
                await Navigation.PushAsync(new HomePage());
            }
            else
            {
                await DisplayAlert("Error", "Unknown role", "OK");
            }
        }

        private async void OnSignupLinkTapped(object sender, EventArgs e)
        {
            
            await Navigation.PushAsync(new SignupPage());
        }
    }
}