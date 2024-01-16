using System;
namespace DataAccess.DTO
{
	public class LoginResponseDTO : Status
	{
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? Expiration { get; set; }
    }
}

