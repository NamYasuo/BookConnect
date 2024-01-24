using System;
namespace BusinessObjects.DTO
{
	public class UserProfile
	{
        public Guid? UserId { get; set; }
        public string? Username { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string? Role { get; set; } = null!;
        public string? Address { get; set; } = null!;
        //public string Rating { get; set; } = null!;
    }
}

