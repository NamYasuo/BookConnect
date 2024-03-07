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

    public class CheckoutDTO
    {
        public Guid AddressId { get; set; }
        public Guid CustomerId { get; set; }
        public List<ProductOptionDTO> Products { get; set; } = null!;
        public PaymentReturnDTO PaymentReturnDTO { get; set; } = null!;
    }

    public class PreSubCheckoutDTO
    {
        public Guid ReferenceId { get; set; }
        public decimal Price { get; set; }
    }

    public class PreCheckoutDTO
    {
        public Guid ReferenceId { get; set; }
        public List<ProductOptionDTO> Products { get; set; } = null!;
    }

    public class ProductOptionDTO
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; } 
    }
}

