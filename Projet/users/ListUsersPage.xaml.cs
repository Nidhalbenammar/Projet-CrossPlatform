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
    public partial class ListUsersPage : ContentPage
    {
        private readonly UserService _userService = new UserService();

        public ListUsersPage()
        {
            InitializeComponent();
            LoadUsers();
        }

        private async void LoadUsers()
        {
            var users = await _userService.GetUsersAsync();
            usersListView.ItemsSource = users;
        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var userId = (long)button.CommandParameter;

            var confirmDelete = await DisplayAlert("Confirm Delete", "Are you sure you want to delete this user?", "Yes", "No");
            if (confirmDelete)
            {
                var isDeleted = await _userService.DeleteUserAsync(userId);
                if (isDeleted)
                {
                    await DisplayAlert("Success", "User deleted successfully", "OK");
                    LoadUsers(); 
                }
                else
                {
                    await DisplayAlert("Error", "Failed to delete user", "OK");
                }
            }
        }
    }
}