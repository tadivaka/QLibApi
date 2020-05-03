using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
namespace QLibrary.Api.Concrete
{
    public class ApiIdentityDbContext : IdentityDbContext
    {
        private AppSettings _appSettings;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// ApiIdentityDbContext
        /// </summary>
        /// <param name="appSettings"></param>
        public ApiIdentityDbContext(IOptions<AppSettings> appSettings, IConfiguration configuration)
        {
            _appSettings = appSettings.Value;
            _configuration = configuration;
        }

        /// <summary>
        /// OnConfiguring
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var conString = _configuration["ConnectionStrings:QLibDatabaseConnection"];
            optionsBuilder.UseSqlServer(conString);
        }
    }
}
