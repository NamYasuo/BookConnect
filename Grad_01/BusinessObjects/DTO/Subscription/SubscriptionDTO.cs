using System;
namespace BusinessObjects.DTO.Subscription
{
	public class NewTierDTO
	{
        public Guid CreatorId { get; set; }
        public decimal Price { get; set; }
        public string TierType { get; set; } = null!;
        public int Duration { get; set; }
        public string Status { get; set; } = null!; //Active or not
    }

    public class NewSubDTO
    {
        public Guid SubcriberId { get; set; }
        public Guid TierId { get; set; }
    }
}

