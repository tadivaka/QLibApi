using System.Collections.Generic;
namespace QLibrary.Api.Concrete
{
    /// <summary>
    /// UserDetails
    /// </summary>
    public class UserDetails
    {
        /// <summary>
        /// GetCurrentUserId
        /// </summary>
        /// <param name="claim"></param>
        /// <returns></returns>
        public static string GetCurrentUserId(IEnumerable<System.Security.Claims.Claim> claim)
        {
            foreach (var item in claim)
            {
                if (item.ToString().Contains("nameidentifier"))
                {
                    return item.Value;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// GetCurrentUserFullName
        /// </summary>
        /// <param name="claim"></param>
        /// <returns></returns>
        public static string GetCurrentUserFullName(IEnumerable<System.Security.Claims.Claim> claim)
        {
            foreach (var item in claim)
            {
                if (item.ToString().Contains("userdata"))
                {
                    return item.Value;
                }
            }
            return "";
        }
    }
}
