using QLibrary.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;


namespace QLibrary.DomainModel
{
    public class DevDbContext: QLibraryDbContext
    {
        private string _currentUser;
        public DevDbContext(string currentUser=""):base()
        {
            this._currentUser = currentUser;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            if (!string.IsNullOrEmpty(env))
            {
                // we set it to optional in the event the environment  file is not found
                builder.AddJsonFile($"appsettings.{env}.json", true);
            }

            var conString = ConfigurationExtensions.GetConnectionString(builder.Build(), "QLibDatabaseConnection");

            optionsBuilder.UseSqlServer(conString);
            optionsBuilder.EnableSensitiveDataLogging();

        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            this.SetAuditFields();
            return (await base.SaveChangesAsync(true, cancellationToken));
        }


        public override int SaveChanges()
        {
            this.SetAuditFields();
            return base.SaveChanges();
        }

        private void SetAuditFields()
        {
            
            #region For Added Entities - Set CreatedBy/CreatedDate
            var addEntities = (from e in ChangeTracker.Entries()
                               where e.State == EntityState.Added
                               select e.Entity);


            foreach (var ent in addEntities)
            {
                var entType = ent.GetType();

                var createdBy = ((TypeInfo)entType).DeclaredProperties
                    .Where(x => x.Name.Trim().ToUpper() == "CREATEDBY")
                    .Select(x => x).FirstOrDefault();

                if (createdBy != null)
                {
                    var tempVal = (string)createdBy.GetValue(ent);
                    if (string.IsNullOrEmpty(tempVal))
                    {
                        createdBy.SetValue(ent, this.GetUserIdFromContext());
                    }
                }

                var createdDate = ((TypeInfo)entType).DeclaredProperties
                    .Where(x => x.Name.Trim().ToUpper() == "CREATEDDATE")
                    .Select(x => x).FirstOrDefault();
                if (createdDate != null)
                {
                    createdDate.SetValue(ent, DateTime.UtcNow);
                }

                //var validationContext = new ValidationContext(entity);
                //Validator.ValidateObject(entity, validationContext);
            }
            #endregion

            #region For Modified Entities - Set ModifiedBy/ModifiedDate
            var modifedEntities = (from e in ChangeTracker.Entries()
                                   where e.State == EntityState.Modified
                                   select e.Entity);

            foreach (var ent in modifedEntities)
            {
                var entType = ent.GetType();

                var modifiedBy = ((TypeInfo)entType).DeclaredProperties
                    .Where(x => x.Name.Trim().ToUpper() == "MODIFIEDBY")
                    .Select(x => x).FirstOrDefault();

                if (modifiedBy != null)
                {
                    var tempVal = (string)modifiedBy.GetValue(ent);
                    if (string.IsNullOrEmpty(tempVal))
                    {
                        modifiedBy.SetValue(ent, this.GetUserIdFromContext());
                    }
                }

                var modifiedDate = ((TypeInfo)entType).DeclaredProperties
                    .Where(x => x.Name.Trim().ToUpper() == "MODIFIEDDATE")
                    .Select(x => x).FirstOrDefault();
                if (modifiedDate != null)
                {
                    modifiedDate.SetValue(ent, DateTime.UtcNow);
                }
            }
            #endregion
        }

        private string GetUserIdFromContext()
        {
            try
            {
                var currentuserid = (string)System.Web.HttpContext.Current.Items["currentuserid"];
                return currentuserid;
            }
            catch
            {
                return "";
            }
        }
    }
}
