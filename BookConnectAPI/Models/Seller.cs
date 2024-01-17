using System.ComponentModel.DataAnnotations;

namespace BookConnectAPI.Models
{
    public class Seller
    {
        public int SellerId { get; set; }

        [Required(ErrorMessage = "SellerName is required.")]
        public string SellerName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }
    }

}
