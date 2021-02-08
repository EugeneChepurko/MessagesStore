using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MessagesStore.ViewModels
{
    public class RegistrationViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Birthday")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Please, enter password")]
        [Display(Name = "Password must contains three character categories: digits, uppercase, lowercase characters and special characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please, confirm password")]
        [Compare("Password", ErrorMessage = "Password is not matched")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}
