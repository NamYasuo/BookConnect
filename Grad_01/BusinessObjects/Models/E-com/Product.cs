using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects.Models
{
	public class Product
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ProductId { get; set; }
		public string Name { get; set; } = null!;
		public string? Description { get; set; }
		public double Price { get; set; }
		public int Quantity { get; set; }
		public DateTime RealeaseDate { get; set; }
	}
}

