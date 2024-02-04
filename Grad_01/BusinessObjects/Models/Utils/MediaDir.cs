using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects.Models.Utils
{
	public class MediaDir
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid MediaId { get; set; }
		public Guid ProductId { get; set; }
		public string? Directory { get; set;}
	}
}

