using System;
using System.ComponentModel.DataAnnotations;
namespace idefny.ViewModels
{
	public class LoginViewModel
	{
		[Required(ErrorMessage="Email is requrid")]
		[EmailAddress]
		public string Email { get; set; }
		[Required(ErrorMessage ="Password is requred")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public bool MyProperty { get; set; }
		[Display(Name ="Remberme")]
		public bool RememberMe { set; get; }

		
	}
}

