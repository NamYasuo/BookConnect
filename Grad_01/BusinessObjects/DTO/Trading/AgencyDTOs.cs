using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Trading
{
    internal class AgencyDTOs
    {
        public Guid? AgencyId { get; set; }
        public string? AgencyName { get; set; } = null!;
    }
}
