using System;
namespace BusinessObjects.DTO
{
    public class NewOrderDTO
    {
        //OrderId
        public Guid OrderId { get; set; }
        //CustomerId
        public Guid CustomerId { get; set; }
        //Status
        public string Status { get; set; } = null!;
        //Notes
        public string? Notes { get; set; }
        //PaymentId
        public Guid PaymentId { get; set; }
        //AddressId
        public Guid AddressId { get; set; }
    }
}

