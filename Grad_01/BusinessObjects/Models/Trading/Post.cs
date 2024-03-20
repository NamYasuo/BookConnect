using BusinessObjects.Models.Ecom.Rating;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObjects.Models.E_com.Trading
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public string? AuthorName { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime? CreatedAt { get; set; }

        [ForeignKey("UserId"), JsonIgnore]
        public virtual AppUser? AppUser { get; set; }
    }
}


