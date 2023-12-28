using car_Sales.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace car_Sales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly Context _context;

        public CarController(Context context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCar()
        {
            try
            {
                var res = await _context.cars.ToListAsync();
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
                var res = await _context.cars.Where(x => x.ID == id).FirstOrDefaultAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateCar([FromBody] Cars cars)
        {
            try
            {
                var res = await _context.AddAsync(cars);
                var change = await _context.SaveChangesAsync();
                if (change > 0)
                {
                    var addedCar = await _context.cars.OrderByDescending(x => x.ID).FirstOrDefaultAsync();
                    return Ok(addedCar);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            try
            {
                var carToDelete = await _context.cars.FindAsync(id);

                if (carToDelete == null)
                {
                    return NotFound(); // Araba bulunamazsa 404 Not Found döndür
                }

                _context.cars.Remove(carToDelete);
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
        public async Task<IActionResult> UpdateCar(int id, [FromBody] Cars updatedCar)
        {
            try
            {
                var existingCar = await _context.cars.FindAsync(id);

                if (existingCar == null)
                {
                    return NotFound(); // Araba bulunamazsa 404 Not Found döndür
                }

                // Güncelleme işlemleri
                existingCar.Brand = updatedCar.Brand;
                existingCar.Model = updatedCar.Model;
                existingCar.Year = updatedCar.Year;
                existingCar.Color = updatedCar.Color;
                existingCar.Price = updatedCar.Price;
                existingCar.Kilometer = updatedCar.Kilometer;
                existingCar.HorsePower = updatedCar.HorsePower;
                existingCar.EngineVolume = updatedCar.EngineVolume;

                _context.cars.Update(existingCar);
                var change = await _context.SaveChangesAsync();

                if (change > 0)
                {
                    return Ok(existingCar); // Başarıyla güncellendiğinde güncellenen arabayı ve 200 OK döndür
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
