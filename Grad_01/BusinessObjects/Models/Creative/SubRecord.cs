using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BusinessObjects.Models.Ecom.Payment;
using Newtonsoft.Json;

namespace BusinessObjects.Models.Creative
{
    public class SubRecord
    {
        [Key]
        public Guid SubRecordId { get; set; }
        public Guid BillingId { get; set; }
        public Guid SubscriptionId { get; set; }
        public string EventType { get; set; } = null!;
        //public string Token { get; set; } = null!;

        [ForeignKey("BillingId"), JsonIgnore]
        public virtual TransactionRecord Transaction { get; set; } = null!;

        [ForeignKey("SubscriptionId"), JsonIgnore]
        public virtual Subscription Subscription { get; set; } = null!;
    }
}

