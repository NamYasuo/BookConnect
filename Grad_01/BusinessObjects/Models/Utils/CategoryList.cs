
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BusinessObjects.Models.Utils
{
	public class CategoryList
	{
		public Guid CategoryId { get; set; }
		public Guid BookId { get; set; }

		[ForeignKey("CategoryId"), JsonIgnore]
		public virtual Category? Category { get; set; }
        [ForeignKey("BookId"), JsonIgnore]
        public virtual Book? Book { get; set; }
	}
}

