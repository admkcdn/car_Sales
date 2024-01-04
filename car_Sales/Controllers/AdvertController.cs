using car_Sales.DataResultForm;
using car_Sales.DTOs;
using car_Sales.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace car_Sales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertController : ControllerBase
    {
        private readonly Context _context;
        private readonly IConfiguration _configuration;

        private readonly string connectionString;

        public AdvertController(Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            connectionString = _configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAdvert()
        {
            try
            {
                List<CarDto> cars = new List<CarDto>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "select * from Get_All_Adverts";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CarDto car = new CarDto
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    CreateDate = Convert.ToDateTime(reader["CreateDate"]),
                                    Title = reader["Title"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    Brand = reader["Brand"].ToString(),
                                    Model = reader["Model"].ToString(),
                                    Year = Convert.ToInt32(reader["Year"]),
                                    Color = reader["Color"].ToString(),
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    Kilometer = Convert.ToInt32(reader["Kilometer"]),
                                    Status = Convert.ToBoolean(reader["Status"]),
                                    HorsePower = Convert.ToInt32(reader["HorsePower"]),
                                    EngineVolume = Convert.ToDecimal(reader["EngineVolume"]),
                                    Name = reader["Name"].ToString(),
                                    Surname = reader["Surname"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Phone = reader["Phone"].ToString(),
                                    Address = reader["Address"].ToString(),
                                    Type = reader["Type"].ToString(),
                                    TransmissionType = reader["TransmissionType"].ToString(),
                                    FuelType = reader["FuelType"].ToString(),
                                    CarImage = reader["CarImage"].ToString()
                                };
                                cars.Add(car);
                            }
                        }
                    }
                }
                return Ok(cars);
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

        [HttpGet("GetDetail")]
        public async Task<IActionResult> GetDetailAdvert([FromQuery] int advertId)
        {
            try
            {
                var res = await _context.adverts.Where(x => x.ID == advertId)
                    .Include(x => x.Cars)
                        .ThenInclude(x => x.TransmissionType)
                    .Include(x => x.Cars)
                        .ThenInclude(x => x.FuelType)
                    .Include(x => x.Cars)
                        .ThenInclude(x => x.Types)
                    .Include(x => x.Users)
                    .FirstOrDefaultAsync();

                var images = await _context.ımages.Where(x => x.Cars == res.Cars).Select(x=>x.imagePath).ToListAsync();
               

                AdvertPhotoDto advertDto = new AdvertPhotoDto()
                {
                    ID = res.ID,
                    Cars = res.Cars,
                    CreateDate = res.CreateDate,
                    Description = res.Description,
                    isActive = res.isActive,
                    Title = res.Title,
                    Users = res.Users,
                    Images = images
                };
                return Ok(advertDto);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAdvert([FromBody] AdvertDto advertDto)
        {
            try
            {
                var advert = new Advert();
                advert.ID = advertDto.ID;
                advert.Title = advertDto.Name;
                advert.Description = advertDto.SubDescription;
                advert.CreateDate = DateTime.Now;
                advert.isActive = true;
                var users = await _context.users.Where(x => x.ID == 2).FirstOrDefaultAsync();
                advert.Users = users!;

                var car = new Cars();
                car.Brand = advertDto.Brand;
                car.Model = advertDto.Model;
                car.Color = advertDto.Color;
                car.HorsePower = Convert.ToInt32(advertDto.Horsepower);
                car.Status = true;
                var fuelType = await _context.fuelTypes.Where(x => x.ID == Convert.ToInt32(advertDto.FuelType.Value)).FirstOrDefaultAsync();
                car.FuelType = fuelType!;
                var transmissionType = await _context.transmissionTypes.Where(x => x.ID == Convert.ToInt32(advertDto.TransmissionType.Value)).FirstOrDefaultAsync();
                car.TransmissionType = transmissionType!;
                var type = await _context.types.Where(x => x.ID == 1).FirstOrDefaultAsync();
                car.Types = type!;
                car.Year = 2020;
                car.Price = Convert.ToInt32(advertDto.Price);
                car.Kilometer = Convert.ToInt32(advertDto.Kilometer);
                car.EngineVolume = 1600;



                var carRes = await _context.cars.AddAsync(car);
                var change = await _context.SaveChangesAsync();
                if (change > 0)
                {
                    List<Image> images = new List<Image>();
                    var addedCar = await _context.cars.OrderByDescending(x => x.ID).FirstOrDefaultAsync();
                    var imageinsert = await _context.ımages.AddAsync(new Image() { Cars = addedCar, imagePath = "C:\\Users\\stjAdem.Kocdogan\\Desktop\\skoda_arac\\skoda1.jpg" });
                    var imageinsert2 = await _context.ımages.AddAsync(new Image() { Cars = addedCar, imagePath = "C:\\Users\\stjAdem.Kocdogan\\Desktop\\skoda_arac\\skoda2.jpg" });
                    var imageinsert3 = await _context.ımages.AddAsync(new Image() { Cars = addedCar, imagePath = "C:\\Users\\stjAdem.Kocdogan\\Desktop\\skoda_arac\\skoda3.jpg" });
                    var imageinsert4 = await _context.ımages.AddAsync(new Image() { Cars = addedCar, imagePath = "C:\\Users\\stjAdem.Kocdogan\\Desktop\\skoda_arac\\skoda4.jpg" });
                    var imageinsert5 = await _context.ımages.AddAsync(new Image() { Cars = addedCar, imagePath = "C:\\Users\\stjAdem.Kocdogan\\Desktop\\skoda_arac\\skoda5.jpg" });
                    var imageinsert6 = await _context.ımages.AddAsync(new Image() { Cars = addedCar, imagePath = "C:\\Users\\stjAdem.Kocdogan\\Desktop\\skoda_arac\\skoda6.jpg" });
                    advert.Cars = addedCar;
                    var addedAdvert = await _context.adverts.OrderByDescending(x => x.ID).FirstOrDefaultAsync();
                }
                var res = await _context.AddAsync(advert);
                var changeAdvert = await _context.SaveChangesAsync();
                return Ok(DataResult<bool>.SuccessResult(true));
            }
            catch (Exception ex)
            {

                return BadRequest(DataResult<bool>.FailureResult(ex.Message));
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
