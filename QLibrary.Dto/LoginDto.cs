using System;
using System.ComponentModel.DataAnnotations;

namespace QLibrary.Dto {
    //TODO: Need to confirm about the login as email 
    public class LoginDto {
       [Required]
       public string Email { get; set; }

       [Required]
       [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
       public string Password { get; set; }
    }
}
