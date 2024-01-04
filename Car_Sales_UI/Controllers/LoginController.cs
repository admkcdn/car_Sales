using car_Sales.DTOs;
using Car_Sales_UI.Controllers.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Car_Sales_UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : BaseController
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public LoginController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        [HttpPost("LoginUser")]
        public async Task<IActionResult> UserLogin(LoginDto loginDto)
        {
            try
            {
                var url = baseUrl + "User/Login";
                var response = await _httpClient.PostAsJsonAsync(url, loginDto);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Ok(await response.Content.ReadAsStringAsync());
                }
                return BadRequest(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet("Me")]
        public async Task<IActionResult> MeCheck()
        {
            try
            {
                var asdfa = LoginUserEmail;
                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
