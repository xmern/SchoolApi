using Microsoft.AspNetCore.Identity;

namespace SchoolApi.Authentication
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public int Password {  get; set; }

    }
}
