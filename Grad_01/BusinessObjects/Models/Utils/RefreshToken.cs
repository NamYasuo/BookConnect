using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BusinessObjects.Models
{
	public class RefreshToken
	{
        [Key]
        public Guid Id { get; set; }
        public string Token { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime ExpiredDate { get; set; }

        public Guid UserId { get; set; }
        [ForeignKey("UserId"), JsonIgnore]
        public virtual AppUser AppUser { get; set; } = null!;
	}
}

