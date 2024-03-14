using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class InventoryManageDTOs
    {
        public Guid InventoryId { get; set; }

        public Guid BookId { get; set; }

        public int Price { get; set; }
        public DateTime DateAdded { get; set; }

        public int Quantity { get; set; }
    }
}
