using System;
namespace BusinessObjects.Models
{
	public class TokenInfo
	{
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string RefreshToken { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime ExpiredDate { get; set; }

        public virtual AppUser AppUser { get; set; } = null!;
	}
}

