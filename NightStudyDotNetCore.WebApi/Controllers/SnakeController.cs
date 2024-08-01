using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NightStudyDotNetCore.SnakeWebApi.Models;
using System.Net.Mime;
using System.Text;
using System.Text.Json.Serialization;
using static NightStudyDotNetCore.SnakeWebApi.Models.SnakeModel;

namespace NightStudyDotNetCore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SnakeController : ControllerBase
    {
        private readonly string _url = "https://burma-project-ideas.vercel.app/snakes";
        private readonly HttpClient _client = new HttpClient();


        [HttpGet]
        public async Task<IActionResult> GetSnakeAsync()
        {
            HttpResponseMessage response = await _client.GetAsync(_url);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                List<SnakeModel> snakes = JsonConvert.DeserializeObject<List<SnakeModel>>(jsonStr)!;
                List<SnakeViewModel> lst = new List<SnakeViewModel>();
                foreach (var snake in snakes)
                {
                    SnakeViewModel item = Change(snake);
                    lst.Add(item);
                }
                return Ok(lst);
            }
            else
            {
                return BadRequest();
            }
;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSnakeAsync(int id)
        {
            var response = await _client.GetAsync($"{_url}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                SnakeModel snakes = JsonConvert.DeserializeObject<SnakeModel>(jsonStr)!;
                var item = Change(snakes);
                return Ok(item);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSnakeAsync(int id, string name, string des, string photo, string isPoison)
        {
            SnakeViewModel snake = new SnakeViewModel()
            {
                Id=id,
                Name = name,
                Detail = des,
                IsPoison = isPoison,
                PhotoUrl = photo
            };
            string jsonSnake = JsonConvert.SerializeObject(snake);
            HttpContent httpcontent = new StringContent(jsonSnake, Encoding.UTF8, MediaTypeNames.Application.Json);
            _client.Timeout = TimeSpan.FromSeconds(200); // Set timeout to 200 seconds
            try
            {
                HttpResponseMessage respone = await _client.PostAsync(_url, httpcontent);
                if (respone.IsSuccessStatusCode)
                {
                    string jsonstr = await respone.Content.ReadAsStringAsync();
                    SnakeViewModel item = JsonConvert.DeserializeObject<SnakeViewModel>(jsonstr)!;
                    return Ok(item);

                }
                else
                {
                    return BadRequest();
                }
            }
            catch(TaskCanceledException ex)
            {
                return Ok(ex);
            }
                   
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSnake(int id, string name, string des, string photo)
        {
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteSnake (int id)
        {
            return Ok();
        }

        private SnakeViewModel Change(SnakeModel snake)
        {
            var model = new SnakeViewModel
            {
                Id = snake.Id,
                Name = snake.MMName,
                Detail = snake.Detail,
                IsPoison = snake.IsPoison,
                PhotoUrl = $"https://burma-project-ideas.vercel.app/snakes/{snake.ImageUrl}"
            };
            return model;
        }
        //public void Generate(int length)
        //{
        //    for (int i = 0; i < length; i++)
        //    {
        //        int rowNo = i + 1;
        //        Create("Name" + rowNo, "Detail" + rowNo, "IsPosition" + rowNo, "PhotoUrl" + rowNo);
        //    }
        //}
    }
}
