
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BusinessObjects.Models.Creative;
using Newtonsoft.Json;

namespace BusinessObjects.Models.Utils
{
	public class CategoryList
	{
		[Key]
		public Guid CategoryListId { get; set; }
		public Guid CategoryId { get; set; }
		public Guid? BookId { get; set; }
		public Guid? WorkId { get; set; }

		[ForeignKey("CategoryId"), JsonIgnore]
		public virtual Category? Category { get; set; }
        [ForeignKey("BookId"), JsonIgnore]
        public virtual Book? Book { get; set; }
		[ForeignKey("WorkId"), JsonIgnore]
		public virtual Work? Work { get; set; }
	}
}

