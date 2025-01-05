using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
namespace idefny.Models
	
{
	public class Users: IdentityUser

	{
		[Required]
		public string FullName { set; get; }
		
	}
}

