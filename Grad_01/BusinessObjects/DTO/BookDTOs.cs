using BusinessObjects.Models;
using BusinessObjects.Models.Ecom.Rating;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class SEODTO
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
    }

    public class BookDetailsDTO
    {
        public Guid ProductId { get; set; }
        public string? Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? PublishDate { get; set; }
        public string? Type { get; set; } = null!;
        public int? Stock { get; set; }
        public double? Rating { get; set; }
        public Guid AgencyId { get; set; }
        public string AgencyName { get; set; } = null!;
    }
}


