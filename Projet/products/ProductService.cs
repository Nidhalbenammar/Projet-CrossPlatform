using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Projet.products
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://192.168.201.59:8080/api/") 
            };
        }

        public async Task<bool> AddProductAsync(string designation, string photo, double price, long categoryId)
        {
            var product = new
            {
                designation = designation,
                photo = photo,
                price = price,
                category = new { id = categoryId }
            };

            var json = JsonConvert.SerializeObject(product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("products/add", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            var response = await _httpClient.GetStringAsync("categories"); 
            return JsonConvert.DeserializeObject<List<Category>>(response);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var response = await _httpClient.GetStringAsync("products"); 
            return JsonConvert.DeserializeObject<List<Product>>(response);
        }

        public async Task<bool> DeleteProductAsync(long productId)
        {
            var response = await _httpClient.DeleteAsync($"products/delete/{productId}"); 
            return response.IsSuccessStatusCode;
        }

        public async Task<Product> GetProductByIdAsync(long productId)
        {
            var response = await _httpClient.GetStringAsync($"products/{productId}"); 
            return JsonConvert.DeserializeObject<Product>(response);
        }

        public async Task<bool> UpdateProductAsync(long productId, string designation, string photo, double price, long categoryId)
        {
            var product = new
            {
                designation = designation,
                photo = photo,
                price = price,
                category = new { id = categoryId }
            };

            var json = JsonConvert.SerializeObject(product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"products/update/{productId}", content); 
            return response.IsSuccessStatusCode;
        }
    }
}
