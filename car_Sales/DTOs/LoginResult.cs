using car_Sales.Models;

namespace car_Sales.DTOs
{
    public class LoginResult
    {
        public string AccessToken { get; set; }
        public Users User { get; set; }
    }
}
