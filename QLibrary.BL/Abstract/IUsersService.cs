using System.Threading.Tasks;

namespace QLibrary.BL.Abstract
{
   public interface IUsersService
   {
      Task<string> GetUserFullName(string userId);
   }
}