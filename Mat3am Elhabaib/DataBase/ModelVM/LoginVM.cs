using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace Mat3am_Elhabaib.DataBase.ModelVM
{
    public class LoginVM
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public  bool RememberMe { get; set; }
    }
}
