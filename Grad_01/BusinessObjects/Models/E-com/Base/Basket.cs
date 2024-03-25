using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BusinessObjects.Models.Ecom;
using Newtonsoft.Json;

namespace BusinessObjects.Models
{
	public class Basket
	{
		[Key]
		public Guid BasketId { get; set; }

		//product_id UNIQUEIDENTIFIER foreign key references Books(ProductId) null,
		public Guid ProductId { get; set; }

		//cart_id UNIQUEIDENTIFIER foreign key references Carts(CartId) null,
		public Guid? CartId { get; set; }

		//order_id UNIQUEIDENTIFIER foreign key references Orders(OrderId) null,
		public Guid? OrderId { get; set; }

		//quantity int,
		public int Quantity { get; set; }

		//added_date datetime,
		public DateTime AddedDate { get; set; }

		//stored_price money NULL,
		public decimal? Stored_Price { get; set; }

		[ForeignKey("ProductId"), JsonIgnore]
		public virtual Book Book { get; set; } = null!;

        [ForeignKey("CartId"), JsonIgnore]
        public virtual Cart? Cart { get; set; }

        [ForeignKey("OrderId"), JsonIgnore]
        public virtual Order? Order { get; set; }
	}
}

