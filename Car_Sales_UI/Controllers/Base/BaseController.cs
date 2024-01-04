using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Car_Sales_UI.Controllers.Base
{
    public class BaseController : ControllerBase
    {
        public const string baseUrl = "https://localhost:7015/api/";

        public string? LoginUserEmail
        {
            get
            {
                var user = User.Identity as ClaimsIdentity;
                if (user != null)
                {
                    var email = user.FindFirst(ClaimTypes.Email);
                    return email != null ? email.Value : null;
                }
                return null;
            }
        }
    }
}
