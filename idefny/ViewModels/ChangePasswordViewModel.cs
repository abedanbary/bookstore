using System;
using System.ComponentModel.DataAnnotations;

namespace idefny.ViewModels
{
	public class ChangePasswordViewModel
	{
        [Required(ErrorMessage = "Email is requird")]
        [EmailAddress]
        public string Email { set; get; }
        [Required(ErrorMessage = "Passsowrd is requrid")]
		[StringLength(40, MinimumLength = 8, ErrorMessage = "not ennoff")]
        [DataType(DataType.Password)]
        [Compare("ConfirmNewPassword", ErrorMessage = ("password does not math."))]
        public string NewPassword { set; get; }
        [Required(ErrorMessage = "Passsowrd confirm is requrid")]
        [DataType(DataType.Password)]
        [Display(Name ="Confirm newPassword")]
        public string ConfirmNewPassword { set; get; }
    }
}
    


