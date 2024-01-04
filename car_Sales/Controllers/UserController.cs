using car_Sales.DataResultForm;
using car_Sales.DTOs;
using car_Sales.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace car_Sales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiBaseController
    {
        private readonly Context _context;
        private readonly IConfiguration _configuration;

        public UserController(Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("GetAll")] //tüm kullanıcıları getirir.
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                var res = await _context.users.ToListAsync();
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
                var res = await _context.users.Where(x => x.ID == id).FirstOrDefaultAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateUser([FromBody] Users users)
        {
            try
            {
                var password = HashPassword(users.Password);
                users.Password = password;
                var res = await _context.AddAsync(users);
                var change = await _context.SaveChangesAsync();
                if (change > 0)
                {
                    var addedUser = await _context.users.OrderByDescending(x => x.ID).FirstOrDefaultAsync();
                    var dataResult = DataResult<Users>.SuccessResult(addedUser!, "Kayıt Başarılı");
                    return Ok(dataResult);
                }
                return BadRequest(DataResult<Users>.FailureResult("Kayıt gerçekleştirilemedi."));
            }
            catch (Exception ex)
            {
                return BadRequest(DataResult<Users>.FailureResult(ex.Message));
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var userToDelete = await _context.users.FindAsync(id);

                if (userToDelete == null)
                {
                    return NotFound(); // Kullanıcı bulunamazsa 404 Not Found döndür
                }

                _context.users.Remove(userToDelete);
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
        public async Task<IActionResult> UpdateUser(int id, [FromBody] Users updatedUser)
        {
            try
            {
                var existingUser = await _context.users.FindAsync(id);

                if (existingUser == null)
                {
                    return NotFound(); // Kullanıcı bulunamazsa 404 Not Found döndür
                }

                // Güncelleme işlemleri
                existingUser.Name = updatedUser.Name;
                existingUser.Surname = updatedUser.Surname;
                existingUser.Email = updatedUser.Email;
                existingUser.Username = updatedUser.Username;
                existingUser.Password = updatedUser.Password;
                existingUser.Phone = updatedUser.Phone;
                existingUser.Address = updatedUser.Address;

                _context.users.Update(existingUser);
                var change = await _context.SaveChangesAsync();

                if (change > 0)
                {
                    return Ok(existingUser); // Başarıyla güncellendiğinde güncellenen kullanıcıyı ve 200 OK döndür
                }

                return BadRequest(); // Bir hata oluştuysa 400 Bad Request döndür
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}"); // İçsel bir hata oluştuysa 500 Internal Server Error döndür
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginDto login)
        {
            try
            {
                var password = HashPassword(login.Password);
                var user = await _context.users.Where(x => x.Email == login.Email && x.Password == password).FirstOrDefaultAsync();
                if (user != null)
                {
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Name, user.Name),
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtExpiryInDays"]));

                    var token = new JwtSecurityToken(
                        _configuration["JwtIssuer"],
                        _configuration["JwtAudience"],
                        claims,
                        expires: expiry,
                        signingCredentials: creds
                    );

                    return Ok(DataResult<LoginResult>.SuccessResult(new LoginResult() { AccessToken = new JwtSecurityTokenHandler().WriteToken(token) ,User = user}));
                }
                else
                {
                    return Ok(DataResult<bool>.SuccessResult(false, "Kullanıcı adı veya şifre hatalı"));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(DataResult<string>.FailureResult(ex.Message));
            }
        }
    }
}
