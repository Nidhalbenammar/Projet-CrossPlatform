using System;
using System.Collections.Generic;

using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Projet.users;
using Xamarin.Forms;

namespace Projet
{

    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://192.168.201.59:8080/api"; 

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            var response = await _httpClient.GetStringAsync($"{BaseUrl}/categories");
            return JsonConvert.DeserializeObject<List<Category>>(response);
        }

        public async Task<List<Product>> GetProductsByCategoryAsync(long categoryId)
        {
            var response = await _httpClient.GetStringAsync($"{BaseUrl}/products/category/{categoryId}");
            return JsonConvert.DeserializeObject<List<Product>>(response);
        }
        public async Task<bool> AddCategoryAsync(string name, string photo)
        {
            var category = new
            {
                name,
                photo
            };

            var json = JsonConvert.SerializeObject(category);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync($"{BaseUrl}/categories/add", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Request failed: " + ex.Message);
                await Application.Current.MainPage.DisplayAlert("Error", "An unexpected error occurred: " + ex.Message, "OK");
                return false;
            }
        }



        public async Task<bool> DeleteCategoryAsync(long categoryId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{BaseUrl}/categories/delete/{categoryId}");
                if (response.IsSuccessStatusCode)
                {
                    return true; 
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Error response: " + errorContent);
                    await Application.Current.MainPage.DisplayAlert("Error", $"Failed to delete category: {errorContent}", "OK");
                    return false; 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Request failed: " + ex.Message);
                await Application.Current.MainPage.DisplayAlert("Error", "An unexpected error occurred: " + ex.Message, "OK");
                return false; 
            }
        }
        public async Task<List<Product>> GetProductsByDesignationAsync(string designation)
        {
            var response = await _httpClient.GetStringAsync($"{BaseUrl}/products/search?designation={designation}");
            return JsonConvert.DeserializeObject<List<Product>>(response);
        }
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            var response = await _httpClient.GetStringAsync($"{BaseUrl}/users/username/{username}");
            return JsonConvert.DeserializeObject<User>(response);
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            var userToUpdate = new
            {
                nom = user.Nom,
                prenom = user.Prenom,
                username = user.Username,
                password = user.Password
            };

            var json = JsonConvert.SerializeObject(userToUpdate);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
         
            var response = await _httpClient.PutAsync($"{BaseUrl}/users/update/{user.Id}", content);
            return response.IsSuccessStatusCode;
        }
        public async Task<List<Commande>> GetCommandesByUsernameAsync(string username)
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"{BaseUrl}/commandes/user/{username}");
                return JsonConvert.DeserializeObject<List<Commande>>(response);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }

}



