using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class BookListingManageDTOs
    {
        public Guid ListingId { get; set; }

        public Guid BookId { get; set; }
        public string ListName { get; set; }
        public string ListDescription { get; set; } = string.Empty;

        public DateTime DateAdded { get; set; }

        public int Quantity { get; set; }
    }
}
