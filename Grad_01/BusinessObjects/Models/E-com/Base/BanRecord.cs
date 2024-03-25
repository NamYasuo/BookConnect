using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BusinessObjects.Models.Ecom.Base
{
	public class BanRecord
	{
		[Key]
		public Guid BanRecordId { get; set; }
		public Guid TargetUserId { get; set; }
		//public Guid AdminId { get; set; }
		public DateTime BannedDate { get; set; }
		public DateTime UnbannedDate { get; set; }
		public string? BanReason { get; set; } 
        public string? UnBanReason { get; set; }

        [ForeignKey("TargetUserId"), JsonIgnore]
		public AppUser TargetedUser { get; set; } = null!;

	}
}

