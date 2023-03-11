using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace posts_microservice.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts");
            var apiResponse = await response.Content.ReadAsStringAsync();
            var postList = JsonConvert.DeserializeObject<List<Post>>(apiResponse)!;

            return Ok(postList);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync($"https://jsonplaceholder.typicode.com/posts/{id}");
            var apiResponse = await response.Content.ReadAsStringAsync();
            var post = JsonConvert.DeserializeObject<Post>(apiResponse)!;

            return Ok(post);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromForm] Post value)
        {
            var post = JsonConvert.SerializeObject(value);
            var content = new StringContent(post, Encoding.UTF8, "application/json");

            using var httpClient = new HttpClient();
            using var response = await httpClient.PostAsync($"https://jsonplaceholder.typicode.com/posts", content);

            return response.EnsureSuccessStatusCode();
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<HttpResponseMessage> Put(int id, [FromForm] Post value)
        {
            var post = JsonConvert.SerializeObject(value);
            var content = new StringContent(post, Encoding.UTF8, "application/json");

            using var httpClient = new HttpClient();
            using var response = await httpClient.PutAsync($"https://jsonplaceholder.typicode.com/posts/{id}", content);

            return response.EnsureSuccessStatusCode();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            using var httpClient = new HttpClient();
            using var response = await httpClient.DeleteAsync($"https://jsonplaceholder.typicode.com/posts/{id}");

            return response.EnsureSuccessStatusCode();
        }
    }
}
