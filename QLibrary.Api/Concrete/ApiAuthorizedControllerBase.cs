using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QLibrary.Api.Concrete
{
    /// <summary>
    /// ApiAuthorizedControllerBase
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ApiAuthorizedControllerBase : ControllerBase
    {
        protected void SetCurrentUserIdToContext()
        {
            var currentUserId = UserDetails.GetCurrentUserId(User.Claims);
            HttpContext.Items["currentuserid"] = currentUserId;
        }
    }
}
