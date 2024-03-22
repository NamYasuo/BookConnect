using System;
using Microsoft.AspNetCore.Http;

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
	    public IFormFile? LogoImg { get; set; } 
		public string Rendezvous { get; set; } = null!;
		public string BusinessType { get; set; } = null!;
    }

	public class AgencyUpdateDTO
	{
        public Guid AgencyId { get; set; }
        public string AgencyName { get; set; } = null!;
        public Guid OwnerId { get; set; }
        public string? PostAddress { get; set; }
        public IFormFile? LogoImg { get; set; }
        public string BusinessType { get; set; } = null!;
    }
}

