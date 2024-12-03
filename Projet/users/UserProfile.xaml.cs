using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Projet.users
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProfile : ContentPage
    {
        private ApiService _apiService;
        private User _currentUser;

        public UserProfile(User currentUser)
        {
            InitializeComponent();
            _apiService = new ApiService();
            _currentUser = currentUser;

            
            FirstNameEntry.Text = currentUser.Nom;
            LastNameEntry.Text = currentUser.Prenom;
            UsernameEntry.Text = currentUser.Username;
            PasswordEntry.Text = currentUser.Password;
            ConfirmPasswordEntry.Text = currentUser.Password;
            LoadCommandes();
        }

        private async void OnSaveChangesButtonClicked(object sender, EventArgs e)
        {
            
            if (SaveChangesButton.Text == "Edit Profile")
            {
                
                FirstNameEntry.IsEnabled = true;
                LastNameEntry.IsEnabled = true;
                UsernameEntry.IsEnabled = true;
                PasswordEntry.IsEnabled = true;
                ConfirmPasswordEntry.IsEnabled = true;
                SaveChangesButton.Text = "Save Changes";
            }
            else 
            {
                if (PasswordEntry.Text != ConfirmPasswordEntry.Text)
                {
                    await DisplayAlert("Error", "Password and confirmation must be the same.", "OK");
                    return; 
                }
                _currentUser.Nom = FirstNameEntry.Text;
                _currentUser.Prenom = LastNameEntry.Text;
                _currentUser.Username = UsernameEntry.Text;
                _currentUser.Password = PasswordEntry.Text;

             
                bool success = await _apiService.UpdateUserAsync(_currentUser);
                if (success)
                {
                    await DisplayAlert("Success", "Profile updated successfully!", "OK");

                   
                    FirstNameEntry.IsEnabled = false;
                    LastNameEntry.IsEnabled = false;
                    UsernameEntry.IsEnabled = false;
                    PasswordEntry.IsEnabled = false;

                    SaveChangesButton.Text = "Edit Profile";
                }
                else
                {
                    await DisplayAlert("Error", "Failed to update profile.", "OK");
                }
            }
        }
        private async void LoadCommandes()
        {
            var commandes = await _apiService.GetCommandesByUsernameAsync(_currentUser.Username);

            if (commandes != null && commandes.Count > 0)
            {
                CommandesListView.ItemsSource = commandes;
                CommandesListView.IsVisible = true;
            }
            else
            {
                CommandesListView.IsVisible = false;
            }
        }
    }
}
