using System;
namespace BusinessObjects.Models
{
	public class Book: Product
	{
        public string? Type { get; set; } = null!;
        public string? Author { get; set; }
        public string? CoverDir { get; set; }
        public string? BackgroundDir { get; set; }
    }
}

