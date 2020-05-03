using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using ssc = System.Security.Claims;
using System.Text;
using QLibrary.Api.Abstract;

namespace QLibrary.Api.Concrete
{
    /// <summary>
    /// Class to Generate JwtToken for the API
    /// </summary>
    public class ApiJwtToken : IApiJwtToken
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public ApiJwtToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Generates JwtToken
        /// </summary>
        /// <param name="email"></param>
        /// <param name="user"></param>
        /// <param name="userFullName"></param>
        /// <returns></returns>
        public string GenerateJwtToken(string email, IdentityUser user, string userFullName)
        {
            var claims = new List<ssc.Claim>
            {
                new ssc.Claim(JwtRegisteredClaimNames.Sub, email),
                new ssc.Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new ssc.Claim(ssc.ClaimTypes.NameIdentifier, user.Id),
                new ssc.Claim(ssc.ClaimTypes.UserData, userFullName)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtIssuer"],
                audience: _configuration["JwtIssuer"],
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
