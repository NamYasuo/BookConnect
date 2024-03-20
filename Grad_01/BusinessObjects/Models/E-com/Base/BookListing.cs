using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace BusinessObjects.Models
{

    public class BookListing
    {
        [Key]
        public Guid ListingId { get; set; }

        public Guid AgencyId { get; set; }

        public Guid BookId { get; set; }
        public string? ListName { get; set; }
        public string ListDescription { get; set; } = string.Empty;

        public DateTime DateAdded { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("AgencyId"), JsonIgnore]
        public virtual Agency Agency { get; set; } = null!;

        [ForeignKey("BookId"), JsonIgnore]
        public virtual Book Book { get; set; } = null!;
      
    }
}
