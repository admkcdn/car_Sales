using car_Sales.DataResultForm;
using car_Sales.DTOs;
using Car_Sales_UI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Car_Sales_UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UserController : BaseController
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public UserController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            var url = baseUrl + "User/Create";
            var response = await _httpClient.PostAsJsonAsync(url, userDto);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(await response.Content.ReadAsStringAsync());
            }
            return BadRequest(await response.Content.ReadAsStringAsync());
        }

        [HttpGet("Test")]
        public async Task<IActionResult> TestGet()
        {
            return Ok("Test Başarılı");
        }
    }
}
