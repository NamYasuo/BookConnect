using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects.Models
{
	public class Category
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid CateId { get; set; }
		public string CateName { get; set; } = null!;
		public string? Description { get; set; }
	}
}

