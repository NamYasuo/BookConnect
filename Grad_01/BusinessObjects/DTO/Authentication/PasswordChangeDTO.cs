using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.DTO
{
	public class PasswordChangeDTO
	{
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
            ErrorMessage = "CurrentPassword must have uppercase letters, lowercase letters, numbers, at least 1 special character and password length greater than or equal to 8 characters")]
        public string CurrentPassword { get; set; } = null!;
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
            ErrorMessage = "NewPassword must have uppercase letters, lowercase letters, numbers, at least 1 special character and password length greater than or equal to 8 characters")]
        public string NewPassword { get; set; } = null!;

        [Required]
        [Compare(nameof(NewPassword), ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = null!;
    }
}

