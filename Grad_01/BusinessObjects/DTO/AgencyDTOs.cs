using System;
namespace BusinessObjects.DTO
{
	public class NameAndIdDTO
	{
		public Guid AgencyId { get; set; }
		public string AgencyName { get; set; } = null!;
	}
}

