using System.ComponentModel.DataAnnotations;

namespace Mat3am_Elhabaib.DataBase.ModelVM
{
    public class EditUser
    {
        [Required(ErrorMessage = "Please Add Your Name..")]
        public string UserName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Location { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string OldPasswrd { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword" , ErrorMessage ="Password Does't Match New Password")]
        public string ConfirmPassword { get; set; }
        

    }
}
