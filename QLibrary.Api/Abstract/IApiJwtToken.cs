using Microsoft.AspNetCore.Identity;
namespace QLibrary.Api.Abstract
{
    public interface IApiJwtToken
    {
        string GenerateJwtToken(string email, IdentityUser user, string userFullName);
    }
}
