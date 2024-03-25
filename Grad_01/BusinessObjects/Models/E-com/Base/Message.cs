using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BusinessObjects.Models
{
    public class Message
    {
        [Key]
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }

        [ForeignKey("SenderId"), JsonIgnore]
        public virtual Agency Sender { get; set; } = null!;

        [ForeignKey("ReceivedId"), JsonIgnore]
        public virtual Agency Receiver { get; set; } = null!;
    }
}
