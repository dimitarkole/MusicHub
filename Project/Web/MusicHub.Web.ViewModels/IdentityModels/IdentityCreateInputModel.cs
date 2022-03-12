namespace MusicHub.Web.ViewModels.IdentityModels
{
    using MusicHub.Common;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class IdentityCreateInputModel
	{

		[Required]
		[MinLength(GlobalConstants.UsernameMinLength)]
		[MaxLength(GlobalConstants.UsernameMaxLength)]
		public string Username { get; set; }

		[Required]
		[MinLength(GlobalConstants.FirstNameMinLength)]
		[MaxLength(GlobalConstants.FirstNameMaxLength)]
		public string FirstName { get; set; }

		[Required]
		[MinLength(GlobalConstants.LastNameMinLength)]
		[MaxLength(GlobalConstants.LastNameMaxLength)]
		public string LastName { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		public DateTime Birthday { get; set; }

		[Compare(nameof(ConfirmPassword))]
		[MinLength(GlobalConstants.PasswordMinLength)]
		[MaxLength(GlobalConstants.PasswordMaxLength)]
		[Required]
		public string Password { get; set; }

		public string ConfirmPassword { get; set; }
    }
}
