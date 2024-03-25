using BusinessObjects.Models.Creative;
using BusinessObjects.Models.E_com.Trading;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObjects.Models.Trading
{
    public class Comment
    {
        [Key]
        public Guid CommentId { get; set; }
        public Guid PostId { get; set; }
        public Guid CommenterId { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

        [ForeignKey("PostId"), JsonIgnore]
        public virtual Post Post { get; set; } = null!;

        [ForeignKey("CommenterId"), JsonIgnore]
        public virtual AppUser? AppUser { get; set; }
    }
}
