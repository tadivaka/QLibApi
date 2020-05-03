using Microsoft.AspNetCore.Identity;

namespace QLibrary.Api.Concrete
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public override string PhoneNumber { get; set; }
        public string CreatedBy { get; set; }
    }
}
