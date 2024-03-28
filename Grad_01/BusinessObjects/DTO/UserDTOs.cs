﻿using System;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;

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
    
    public class UserProfile
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string? Phone { get; set; }

        public IFormFile? AvatarDir { get; set; }
    }
    public class ProfileUserDTO
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string? Phone { get; set; }

        public string AvatarDir { get; set; }
    }
}

