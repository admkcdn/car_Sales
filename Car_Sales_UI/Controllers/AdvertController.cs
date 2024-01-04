using car_Sales.DTOs;
using Car_Sales_UI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Car_Sales_UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AdvertController : BaseController
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public AdvertController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        [HttpPost("AddAdvert")]
        public async Task<IActionResult> CreateAdvert([FromBody] AdvertDto advertDto)
        {
            var url = baseUrl + "Advert/Create";
            var response = await _httpClient.PostAsJsonAsync(url, advertDto);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(await response.Content.ReadAsStringAsync());
            }
            return BadRequest(await response.Content.ReadAsStringAsync());
        }

        [HttpGet("GetAdverts")]
        public async Task<IActionResult> GetAllAdverts()
        {
            var url = baseUrl + "Advert/GetAll";
            var response = await _httpClient.GetAsync(url);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(await response.Content.ReadAsStringAsync());
            }
            return BadRequest(await response.Content.ReadAsStringAsync());
        }

        [HttpGet("Detail")]
        public async Task<IActionResult> GetAdvertDetail([FromQuery]int advertId)
        {
            var url = baseUrl + "Advert/GetDetail?advertId="+advertId;
            var response = await _httpClient.GetAsync(url);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(await response.Content.ReadAsStringAsync());
            }
            return BadRequest(await response.Content.ReadAsStringAsync());
        }
    }
}
