using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Trading
{
    public class AddPostDTOs
    {
        public Guid UserId { get; set; }
        public string? AuthorName { get; set; } = null!;
        public IFormFile? ProductImgs { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public List<AddPostDTOs> CateId { get; set; } = null!;
    }

    public class UpdatePostDTOs
    {
        public Guid PostId { get; set; }
        public string? AuthorName { get; set; } = null!;
        public IFormFile? ProductImgs { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public List<AddPostDTOs> CateId { get; set; } = null!;
    }

}
