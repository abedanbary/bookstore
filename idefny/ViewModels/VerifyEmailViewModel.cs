using System;
using System.ComponentModel.DataAnnotations;

namespace idefny.ViewModels
{
	public class VerifyEmailViewModel
	{
        [Required(ErrorMessage = "Email is requird")]
        [EmailAddress]
        public string Email { set; get; }
    }
}

