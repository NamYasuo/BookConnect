using System;
namespace BusinessObjects.DTO
{
	public class UserProfile
	{
        public Guid? UserId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public string? Address { get; set; }
        public bool IsValidated { get; set; }
        public bool IsSeller { get; set; }
        //public string Rating { get; set; } = null!;
    }

    public class UserValidationDTO
    {
        public Guid userId { get; set; }
        public bool choice { get; set; }
    }
}

