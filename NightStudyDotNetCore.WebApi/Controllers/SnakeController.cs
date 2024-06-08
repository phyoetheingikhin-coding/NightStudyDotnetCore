using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NightStudyDotNetCore.SnakeWebApi.Models;
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
        public async Task <IActionResult> GetSnakeAsync()
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
            var response= await _client.GetAsync($"{_url}/{id}");
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

        [HttpPost()]
        public IActionResult CreateSnake(string name, string des, string photo)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSnake(int id, string name, string des, string photo)
        {
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult UpdateSnake(int id)
        {
            return Ok();
        }
        
        private SnakeViewModel Change(SnakeModel snake)
        {
            var model = new SnakeViewModel
            {
                Id=snake.Id,
                Name = snake.MMName,
                Detail = snake.Detail,
                IsPoison = snake.IsPoison,
                PhotoUrl = $"https://burma-project-ideas.vercel.app/snakes/{snake.ImageUrl}"
            };
            return model;
        }
    }
}
