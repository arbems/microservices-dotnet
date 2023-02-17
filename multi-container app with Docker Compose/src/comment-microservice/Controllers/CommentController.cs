using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Newtonsoft.Json;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace comment_microservice.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/comments");
            var apiResponse = await response.Content.ReadAsStringAsync();
            var commentList = JsonConvert.DeserializeObject<List<Comment>>(apiResponse)!;

            return Ok(commentList);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            using var httpClient = new HttpClient();            
            using var response = await httpClient.GetAsync($"https://jsonplaceholder.typicode.com/comments/{id}");
            var apiResponse = await response.Content.ReadAsStringAsync();
            var comment = JsonConvert.DeserializeObject<Comment>(apiResponse)!;
            
            return Ok(comment);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromForm] Comment value)
        {
            var comment = JsonConvert.SerializeObject(value);
            var content = new StringContent(comment, Encoding.UTF8, "application/json");

            using var httpClient = new HttpClient();
            using var response = await httpClient.PostAsync($"https://jsonplaceholder.typicode.com/comments", content);

            return response.EnsureSuccessStatusCode();
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<HttpResponseMessage> Put(int id, [FromForm] Comment value)
        {
            var comment = JsonConvert.SerializeObject(value);
            var content = new StringContent(comment, Encoding.UTF8, "application/json");

            using var httpClient = new HttpClient();
            using var response = await httpClient.PutAsync($"https://jsonplaceholder.typicode.com/comments/{id}", content);

            return response.EnsureSuccessStatusCode();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            using var httpClient = new HttpClient();
            using var response = await httpClient.DeleteAsync($"https://jsonplaceholder.typicode.com/comments/{id}");

            return response.EnsureSuccessStatusCode();
        }
    }
}
