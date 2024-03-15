using System;
namespace BusinessObjects.DTO
{
	public class NameAndIdDTO
	{
		public Guid AgencyId { get; set; }
		public string AgencyName { get; set; } = null!;
	}

	public class AgencyRegistrationDTO
	{
		public Guid OwnerId { get; set; }
		public string AgencyName { get; set; } = null!;
		public string Rendezvous { get; set; } = null!;
		public string BusinessType { get; set; } = null!;
    }
}

