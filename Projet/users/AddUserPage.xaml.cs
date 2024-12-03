using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Projet.users
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddUserPage : ContentPage
    {
        private readonly UserService _userService = new UserService();

        public AddUserPage()
        {
            InitializeComponent();
        }

        private async void OnAddUserClicked(object sender, EventArgs e)
        {
            string role ="USER";

            bool isAdded = await _userService.AddUserAsync(
                NomEntry.Text,
                PrenomEntry.Text,
                UsernameEntry.Text,
                PasswordEntry.Text,
                role
            );

            if (isAdded)
            {
                await DisplayAlert("Success", "User added successfully", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Failed to add user", "OK");
            }
        }
    }
}