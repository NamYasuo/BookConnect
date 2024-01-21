using System;
using BusinessObjects.Models;
using BusinessObjects.Models.Ecom;

namespace APIs.DTO.Ecom
{
	public class ProductToCartDTO
	{
		public Guid ProductId { get; set; }
		public Guid CartId { get; set; }
		public int Quantity { get; set; }
	}

	public class CartDetailsDTO
	{
        // B.[ProductId], B.[Price],  B.[Quantity] AS Stock, B.Name,LP.[quantity],C.[CartId]

        public Guid? ProductId { get; set; }
        public double? Price { get; set; }
        public int? Stock { get; set; }
        public string? Name { get; set; } = null!;
        public int? Quantity { get; set; }
		public Guid? CartId { get; set;}

		//public List<Category>? Categories { get; set; }
	}

}

