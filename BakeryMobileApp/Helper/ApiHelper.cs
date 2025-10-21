using BakeryMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BakeryMobileApp.Helper
{
    public class ApiHelper
    {
        HttpClient httpClient = new HttpClient();
        private readonly string url = "http://192.168.1.198:5168/api/";

        public async Task<Customer> LoginResult(LoginRequests loginRequests) 
        {
            var result = await httpClient.PostAsJsonAsync(url + "login", loginRequests);
            if (result.IsSuccessStatusCode)
                return await result.Content.ReadFromJsonAsync<Customer>();
            return new Customer();
        }

        public async Task<List<Product>> GetProducts() 
        {
            var result = await httpClient.GetFromJsonAsync<List<Product>>(url + "products");
            return result;
        }
        public async Task<List<Category>> GetCategories() 
        {
            return await httpClient.GetFromJsonAsync<List<Category>>(url + "categories");
        }
        public async Task<List<Transaction>> GetUserOrders(int id) 
        {
            var result = await httpClient.GetAsync(url + $"orders/{id}");
            if (result.StatusCode == HttpStatusCode.OK)
                return await result.Content.ReadFromJsonAsync<List<Transaction>>();
            return new List<Transaction>();
        }
        public async Task<Customer> GetCustomerById(int id) 
        {
            var result = await httpClient.GetAsync(url + $"customer/{id}");
            if (result.StatusCode == HttpStatusCode.OK)
                return await result.Content.ReadFromJsonAsync<Customer>();
            return new Customer();
        }
        public async Task<bool> PlaceOrder(Transaction transaction) 
        {
            var result = await httpClient.PostAsJsonAsync(url + "orders", transaction);
            if(result.StatusCode == HttpStatusCode.OK)
                return true;
            return false;
        }
    }
}
