using System.Linq;
using QLibrary.DomainModel.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


namespace QLibrary.DomainModel.Concrete
{
   public class EfUsersRepository : IUsersRepository
   {
      public async Task<string> GetUserFullName(string userId)
      {
         using (var db = new DevDbContext())
         {
            var resUser = await db.AspNetUsers.Where(x => x.Id == userId).Select(x => new { x.UserName }).FirstOrDefaultAsync();
            return resUser.UserName + " " + resUser.UserName;
         }
      }
   }
}
