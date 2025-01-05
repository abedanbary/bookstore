using System.ComponentModel.DataAnnotations;
namespace idefny.ViewModels
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage ="name isrequrid")]
		public string Name { set; get; }
		[Required(ErrorMessage ="Email is requird")]
		[EmailAddress]
		public string Email { set; get; }
		[Required(ErrorMessage ="Passsowrd is requrid")]
		[StringLength(40,MinimumLength =8,ErrorMessage ="not ennoff")]
		[DataType(DataType.Password)]
		[Compare("ConfirmPassword",ErrorMessage =("password does not math."))]
		public string Password { set; get; }
        [Required(ErrorMessage = "Passsowrd confirm is requrid")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { set; get; }
    }
}

