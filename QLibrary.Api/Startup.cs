using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Swagger;
using QLibrary.Api.Concrete;
using QLibrary.Api.Abstract;
using AppSettings =QLibrary.Api.Concrete.AppSettings;
using QLibrary.BL.Abstract;
using QLibrary.BL.Concrete;
using Microsoft.AspNetCore.Http;
using System.Web;
using QLibrary.DomainModel.Abstract;
using QLibrary.DomainModel.Concrete;

namespace QLibrary.Api
{
   public class Startup
   {
      public Startup(IConfiguration configuration)
      {
         Configuration = configuration;
      }

      public IConfiguration Configuration
      {
         get;
      }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {

         var appSettings = Configuration.GetSection("AppSettings");
         services.Configure<AppSettings>(appSettings);

         // ===== Add our DbContext ========
         services.AddDbContext<ApiIdentityDbContext>();

         // // ======= Add our DatabaseContext ======
         //services.AddDbContext<DevDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("QLibDatabaseConnection")));

         // ===== Add Identity ========
         services.AddIdentity<IdentityUser, IdentityRole>()
             .AddEntityFrameworkStores<ApiIdentityDbContext>()
             .AddDefaultTokenProviders();

         // ===== Add Jwt Authentication ========
         JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
         services
             .AddAuthentication(options =>
             {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

             })
             .AddJwtBearer(cfg =>
             {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                   ValidIssuer = Configuration["JwtIssuer"],
                   ValidAudience = Configuration["JwtIssuer"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                   ClockSkew = TimeSpan.Zero // remove delay of token when expire
                   };
             });


         // ===== Add service and create Policy with options ========
         services.AddCors(options =>
         {
            options.AddPolicy("CorsPolicy",
                   builder => builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials());
         });

         services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
         services.AddRouting();
         services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

         services.AddScoped<IApiJwtToken, ApiJwtToken>();
         //Repositories
         services.AddSingleton<IUsersRepository, EfUsersRepository>();

         services.AddSingleton<IUsersService, UsersService>();

         // Register the Swagger generator, defining 1 or more Swagger documents
         services.AddSwaggerGen(c =>
         {
            c.SwaggerDoc("v1", new Info { Title = "QLibrary.Api", Version = "v1" });
         });
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApiIdentityDbContext apiIdentityDbContext, ILoggerFactory loggerFactory)
      {
         //loggerFactory.AddLog4Net();
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }
         // Enable middleware to serve generated Swagger as a JSON endpoint.
         app.UseSwagger();

         // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
         // specifying the Swagger JSON endpoint.
         app.UseSwaggerUI(c =>
         {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "QLibrary.Api V1");
            c.RoutePrefix = string.Empty;
         });

         // Enable Cors
         // This is a global policy - assigned here OR we can assign on each controller
         // As of Now we assigned globla policy
         app.UseCors("CorsPolicy");

         // ===== Add Log4Net ======
         loggerFactory.AddLog4Net();
         app.UseStaticHttpContext();
         app.UseMvc();

         // ===== Create tables ======
         apiIdentityDbContext.Database.EnsureCreated();
      }
   }
}
