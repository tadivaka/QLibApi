using System.Threading.Tasks;

namespace QLibrary.DomainModel.Abstract
{
   public interface IUsersRepository
   {
      Task<string> GetUserFullName(string userId);
   }
}
