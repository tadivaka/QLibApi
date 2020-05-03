using System;
using System.Linq;
using QLibrary.Api.Abstract;
using Microsoft.Extensions.Logging;
using QLibrary.Common.Extensions;
using QLibrary.Common.Enums;
using System.Threading.Tasks;
using QLibrary.DomainModel;
using QLibrary.Api.Utilities;
using QLibrary.BL.Abstract;
using QLibrary.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace QLibrary.Api.Controllers
{

    /// <summary>
    /// Account Controller
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IApiJwtToken _apiJwtToken;
        private readonly IUsersService _usersService;
      

        /// <summary>
        /// Account Controller
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="configuration"></param>
        /// <param name="apiJwtToken"></param>
        /// <param name="logger"></param>
        /// <param name="clientService"></param>
        /// <param name="usersService"></param>
        /// <param name="emailService"></param>
        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration,
            IApiJwtToken apiJwtToken,
            IUsersService usersService,
            ILogger<AccountController> logger
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _apiJwtToken = apiJwtToken;
            _usersService = usersService;
            _logger = logger;
        }
      
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            _logger.WriteLogInformation(ComponentLayer.API, $"{nameof(AccountController)}", $"{nameof(Login)}", MethodLine.BEGIN, loginDto.Email);
            var result = _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, false, false).Result;

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => String.Equals(r.Email.Trim(), loginDto.Email.Trim(), StringComparison.CurrentCultureIgnoreCase));
                //var details = await _clientService.GetUserDetails(appUser.Id);
               
                
                if (appUser.LockoutEnabled == false)
                {
                    return BadRequest(new
                    {
                        message = Messages.AccountLocked,
                        IsAccountLocked = true
                    });
                }

                var userFullName = await _usersService.GetUserFullName(appUser.Id);
                var jwtToken = _apiJwtToken.GenerateJwtToken(loginDto.Email, appUser, userFullName);
                using (var db = new DevDbContext())
                {
                    var efUser = db.AspNetUsers.First(x => x.Id == appUser.Id);
                    if (efUser != null)
                    {
                        efUser.LockoutEnabled = true;
                        efUser.AccessFailedCount = 0;
                    }
                    await db.SaveChangesAsync();
                }
                _logger.WriteLogInformation(ComponentLayer.API, $"{nameof(AccountController)}", $"{nameof(Login)}", MethodLine.END, loginDto.Email);
                return Ok(new
                {
                    token = jwtToken,
                   // firstName = details.FirstName,
                  //  details.roleId,
                  //  clientId = details.ClientId,
                    userId = appUser.Id,
                  //  IsPwdVerified = details.isPwdVerified,
                  //  clientName = details.ClientName,
                   // id = details.Id
                });
            }
            else
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.Email.Trim().ToLower() == loginDto.Email.Trim().ToLower());
                if (appUser == null)
                {
                    return BadRequest(new
                    {
                        message = Messages.InvalidEmail,
                        IsAccountLocked = false
                    });
                }
                else
                {
               var maxAccessFailedCount = 10;
              
               
                  return BadRequest(new
                  {
                     message = Messages.AccountLocked,
                     IsAccountLocked = true
                  });
            }
         }
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto registerDto)
        {
            _logger.WriteLogInformation(ComponentLayer.API, $"{nameof(AccountController)}", $"{nameof(RegisterAsync)}", MethodLine.BEGIN, registerDto.Email);
            var user = new IdentityUser
            {
                UserName = registerDto.Email,
                Email = registerDto.Email
            };
            var result = _userManager.CreateAsync(user, registerDto.Password).Result;
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                string userFullName = await _usersService.GetUserFullName(user.Id);
                var jwtToken = _apiJwtToken.GenerateJwtToken(registerDto.Email, user, userFullName);
                _logger.WriteLogInformation(ComponentLayer.API, $"{nameof(AccountController)}", $"{nameof(RegisterAsync)}", MethodLine.END, registerDto.Email);
                return Ok(new
                {
                    token = jwtToken
                });
            }
            _logger.WriteLogError(ComponentLayer.API, $"{nameof(AccountController)}", $"{nameof(RegisterAsync)}", registerDto.Email);
            return BadRequest(Messages.InvalidDetails);
        }
    }
}