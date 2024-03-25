using System;
using BusinessObjects.Models;

namespace BusinessObjects.DTO
{
	public class UserProfileDTO
	{
        public Guid? UserId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public string? Address { get; set; }
        public bool IsValidated { get; set; }
        public bool IsSeller { get; set; }
        public bool IsBanned { get; set; }
        public List<Agency>? Agencies { get; set; }
        //public string Rating { get; set; } = null!;
    }

    public class UserValidationDTO
    {
        public Guid userId { get; set; }
        public bool choice { get; set; }
    }

    public class BanUserDTO
    {
        public Guid UserId { get; set; }
        public string Reason{ get; set; } = null!;
        public TimeSpan? Duration { get; set; }
    }

    public class NewAddressDTO
    {
        public Guid AddressId { get; set; }
        public string? City_Province { get; set; }
        public string? District { get; set; } 
        public string? SubDistrict { get; set; }
        public string Rendezvous { get; set; } = null!;
        public bool Default { get; set; } = false; 
        public Guid? UserId { get; set; }
    }
}

