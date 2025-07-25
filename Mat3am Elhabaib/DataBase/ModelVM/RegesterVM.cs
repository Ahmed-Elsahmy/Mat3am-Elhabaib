﻿using System.ComponentModel.DataAnnotations;

namespace Mat3am_Elhabaib.DataBase.ModelVM
{
    public class RegesterVM
    {
        [Required(ErrorMessage ="Please Add Your Name..")]
        public  string UserName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Location { get; set; }
        [Required]
        [Phone]
        [MinLength(11 , ErrorMessage ="Phone Number Must be 11 number"),MaxLength(11 , ErrorMessage = "Phone Number Must be 11 number")]
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.Password)]   
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
