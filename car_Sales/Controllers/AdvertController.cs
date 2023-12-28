using car_Sales.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace car_Sales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertController : ControllerBase
    {
        private readonly Context _context;

        public AdvertController(Context context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAdvert()
        {
            try
            {
                var res = await _context.adverts.ToListAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            try
            {
                var res = await _context.adverts.Where(x => x.ID == id).FirstOrDefaultAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAdvert([FromBody] Advert advert)
        {
            try
            {
                var res = await _context.AddAsync(advert);
                var change = await _context.SaveChangesAsync();
                if (change > 0)
                {
                    var addedAdvert = await _context.adverts.OrderByDescending(x => x.ID).FirstOrDefaultAsync();
                    return Ok(addedAdvert);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteAdvert(int id)
        {
            try
            {
                var advertToDelete = await _context.adverts.FindAsync(id);

                if (advertToDelete == null)
                {
                    return NotFound(); // İlan bulunamazsa 404 Not Found döndür
                }

                _context.adverts.Remove(advertToDelete);
                var change = await _context.SaveChangesAsync();

                if (change > 0)
                {
                    return Ok(); // Başarıyla silindiğinde 200 OK döndür
                }

                return BadRequest(); // Bir hata oluştuysa 400 Bad Request döndür
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}"); // İçsel bir hata oluştuysa 500 Internal Server Error döndür
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateAdvert(int id, [FromBody] Advert updatedAdvert)
        {
            try
            {
                var existingAdvert = await _context.adverts.FindAsync(id);

                if (existingAdvert == null)
                {
                    return NotFound(); // İlan bulunamazsa 404 Not Found döndür
                }

                // Güncelleme işlemleri
                existingAdvert.CreateDate = updatedAdvert.CreateDate;
                existingAdvert.isActive = updatedAdvert.isActive;
                existingAdvert.Title = updatedAdvert.Title;
                existingAdvert.Description = updatedAdvert.Description;

                _context.adverts.Update(existingAdvert);
                var change = await _context.SaveChangesAsync();

                if (change > 0)
                {
                    return Ok(existingAdvert); // Başarıyla güncellendiğinde güncellenen ilanı ve 200 OK döndür
                }

                return BadRequest(); // Bir hata oluştuysa 400 Bad Request döndür
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}"); // İçsel bir hata oluştuysa 500 Internal Server Error döndür
            }
        }
    }
}
