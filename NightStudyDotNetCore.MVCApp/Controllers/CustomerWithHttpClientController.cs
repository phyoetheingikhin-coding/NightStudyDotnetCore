using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NightStudyDotNetCore.MVCApp.Models;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace NightStudyDotNetCore.MVCApp.Controllers
{
    public class CustomerWithHttpClientController : Controller
    {
       
        private readonly HttpClient _httpClient;
       

        public CustomerWithHttpClientController()
        {
            
            _httpClient = new HttpClient();
        }


        [ActionName("Index")]
        public async Task<IActionResult> CustomerIndex()
        {
            HttpResponseMessage respone = await _httpClient.GetAsync("api/customer");
            CustomerModel model = new CustomerModel();
            if (respone.IsSuccessStatusCode)
            {
                string jsonstr = await respone.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<CustomerModel>(jsonstr)!;
            }
            return View("CustomerIndex", model);
        }



        [ActionName("Edit")]
        public async Task<IActionResult> CustomerEdit(int id)
        {
            var response = await _httpClient.GetAsync($"api/customer/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return Redirect("/Customer");
            }
            var jsonStr = await response.Content.ReadAsStringAsync();
            CustomerModel model = JsonConvert.DeserializeObject<CustomerModel>(jsonStr)!;
            return View("CustomerEdit", model);

        }


        [ActionName("Create")]
        public IActionResult CustomerCreate()
        {
            return View("CustomerCreate");

        }

        [HttpPost]

        [ActionName("Save")]
        public async Task<IActionResult> CustomerSave(CustomerModel customer)
        {
            HttpContent content = new StringContent(content: JsonConvert.SerializeObject(customer), Encoding.UTF8, Application.Json);
            await _httpClient.PostAsync("api/customer", content);
            return Redirect("/Customer");
        }


        [ActionName("Update")]
        public async Task <IActionResult> CustomerUpdate(int id, CustomerModel customer)
        {
            HttpContent content = new StringContent(content: JsonConvert.SerializeObject(customer), Encoding.UTF8, Application.Json);
            var response = await _httpClient.PutAsync($"api/customer/{id}", content);
            if (!response.IsSuccessStatusCode)
            {
                return Redirect("/Customer/CustomerIndex/");
            }
            return Redirect("/Customer");

        }


        [ActionName("Delete")]
        public async Task<IActionResult> CustomerDelete(int id)
        {
            await _httpClient.DeleteAsync($"api/customer/{id}");
            return Redirect("/Customer");
        }
    }
}
