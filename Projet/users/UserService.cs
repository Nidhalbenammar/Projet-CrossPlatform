using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Projet.users
{
    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://192.168.201.59:8080/api/") 
            };
        }

        public async Task<bool> AddUserAsync(string nom, string prenom, string username, string password, string role)
        {
            var user = new
            {
                nom,
                prenom,
                username,
                password,
                role
            };

            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("users/signup", content); 
            return response.IsSuccessStatusCode;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var response = await _httpClient.GetStringAsync("users");
            return JsonConvert.DeserializeObject<List<User>>(response);
        }

        public async Task<bool> DeleteUserAsync(long userId)
        {
            var response = await _httpClient.DeleteAsync($"users/delete/{userId}"); 
            return response.IsSuccessStatusCode;
        }
    }
}
