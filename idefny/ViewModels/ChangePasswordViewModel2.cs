using System.ComponentModel.DataAnnotations;

namespace idefny.ViewModels
{
    public class ChangePasswordViewModel2
    {
        public string UserId { get; set; } // This property is used to identify the user whose password is being changed

        public string CurrentPassword { get; set; } // Optional if admins are not required to enter it

        public string NewPassword { get; set; } // The new password to be set

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The confirmation password does not match.")]
        public string ConfirmNewPassword { get; set; } // Confirmation of the new password
    }
}
    

