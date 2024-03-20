using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class AdsManageDTOs
    {
        public Guid AdsId { get; set; }

        public required string Donors { get; set; }
        public string Description { get; set; } = string.Empty;

        public DateTime DateAdded { get; set; }
    }
}
