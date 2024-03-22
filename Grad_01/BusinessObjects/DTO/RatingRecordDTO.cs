using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class RatingRecordDTO
    {
        public Guid RatingId { get; set; }
        public Guid UserId { get; set; }
        public int RatingPoint { get; set; }
        public string Comment { get; set; }
    }
}
