﻿using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace car_Sales.Controllers
{
    public class ApiBaseController:ControllerBase
    {
        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2")); 
                }
                return builder.ToString();
            }
        }
    }
}
