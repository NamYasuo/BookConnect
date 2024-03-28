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
    public class PostInterest
    {
        [Key]
        public Guid PostInterestId { get; set; }
        public Guid InteresterId { get; set; }
        public Guid PostId { get; set; }

        [ForeignKey("PostId"), JsonIgnore]
        public virtual Post Post { get; set; }

        [ForeignKey("InteresterId"), JsonIgnore]
        public virtual AppUser? AppUser { get; set; }
    }
}
