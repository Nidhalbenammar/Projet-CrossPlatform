using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Projet
{
    public class CommandeService
    {
        private readonly HttpClient _httpClient;

        public CommandeService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://192.168.201.59:8080/api/commandes/");
        }

        public async Task<bool> AddCommandeAsync(string username, List<CartItem> cartItems)
        {
            foreach (var item in cartItems)
            {
                var commande = new
                {
                    designation = item.Designation,
                    price = item.Price,
                    quantity = item.Quantity,
                    username = username
                };

                var json = JsonConvert.SerializeObject(commande);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("add", content);

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
