using System.Threading.Tasks;
using QLibrary.BL.Abstract;
using QLibrary.DomainModel.Abstract;


namespace QLibrary.BL.Concrete
{
   public class UsersService : IUsersService
   {
      private readonly IUsersRepository usersRepository;
      public UsersService(IUsersRepository usersRepository)
      {
         this.usersRepository = usersRepository;
      }

      public Task<string> GetUserFullName(string userId)
      {
         return usersRepository.GetUserFullName(userId);
      }
   }
}