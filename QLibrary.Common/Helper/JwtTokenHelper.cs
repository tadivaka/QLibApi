using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;


namespace QLibrary.Common.Helper
{
    public class JwtTokenHelper
    {
        /// <summary>
        /// Get User Id from JwtToken
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string GetUserId()
        {
            var token = string.Empty;
            return token;
            //try
            //{
            //    var handler = new JwtSecurityTokenHandler();
            //    var securityToken = handler.ReadJwtToken(token);

            //    var userId = securityToken.Claims.ToList()[2].Value;//.First(c => c.Type.ToUpper() == "NAMEIDENTIFIER").Value;

            //    return userId;
            //}
            //catch (Exception ex)
            //{
            //    Trace.Write("Exception Occurred in Getting the UserId from token");
            //    Trace.Write(ex.Message);
            //    return null;
            //}
        }
    }
}
