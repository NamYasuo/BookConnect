using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Trading
{
    public class PostDTOs
    {
        public Guid? PostId { get; set; }
        public string? AuthorName { get; set; } = null!;
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime? CreatedAt  { get; set; }
    }
}
