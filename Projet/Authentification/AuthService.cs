using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using Projet.users;

namespace Projet
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("http://192.168.201.59:8080/api/users/") }; 
        }

        public async Task<bool> Signup(string nom, string prenom, string username, string password)
        {
            var user = new
            {
                nom,
                prenom,
                username,
                password,
                role = "USER"
            };

            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("signup", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<User> Login(string username, string password)
        {
            var loginDetails = new
            {
                username,
                password
            };

            var content = new StringContent(JsonConvert.SerializeObject(loginDetails), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("login", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<User>(jsonResponse); 
            }

            return null; 
        }

    }
}
