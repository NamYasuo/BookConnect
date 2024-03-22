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

    public class NewSubRecordDTO
    {
        public Guid SubcriberId { get; set; }
        public Guid TierId { get; set; }
        public string EventType { get; set; } = string.Empty;
        public PaymentReturnDTO? PaymentReturnDTO { get; set; }
    }

    public class SubRecordDetailsDTO
    {
        public Guid SubRecordId { get; set; }
        public Guid BillingId { get; set; }
        public Guid SubscriptionId { get; set; }
        public string TierName { get; set; } = string.Empty;
        public string CreatorName { get; set; } = string.Empty;
        public string EventType { get; set; } = null!;

    }
}


