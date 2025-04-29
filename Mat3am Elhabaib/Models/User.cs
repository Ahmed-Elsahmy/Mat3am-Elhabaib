using Microsoft.AspNetCore.Identity;

namespace Mat3am_Elhabaib.Models
{
    public class User: IdentityUser
    {
        public string? FullName { get; set; }
        public string PhoneNumber { get; set; }
        public  string Location { get; set; }
    }
}
