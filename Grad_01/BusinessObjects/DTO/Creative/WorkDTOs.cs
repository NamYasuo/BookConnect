using System;
using Microsoft.AspNetCore.Http;


namespace BusinessObjects.DTO
{

    //------------------------------WORK ZONE-----------------------------------//
    public class NewWorkDTO
	{
		public Guid AuthorId { get; set; }
		public string Title { get; set; } = null!;
		public string Type { get; set; } = null!;
		public string Status { get; set; } = null!;
		public IFormFile? Cover { get; set; }
		public IFormFile? Background { get; set; } 
		public string? Description { get; set; } 
	}
	public class WorkDetailsDTO
	{
        public Guid WorkId { get; set; }
        public string Title { get; set; } = null!;
		public string? Author { get; set; } = null!;
        public string Type { get; set; } = null!; //Values: Public or Private
        public string Status { get; set; } = null!; //Values: Published or not
        public string? CoverDir { get; set; }
        public string? Price { get; set; }
        public string? BackgroundDir { get; set; }
        public string? Description { get; set; }
    }

    public class WorkIdTitleDTO
    {
        public Guid WorkId { get; set; }
        public string? Title { get; set; }
    }

    public class CheckWorkOwnerDTO
    {
        public Guid WorkId { get; set; }
        public Guid UserId { get; set; }
    }

    public class SetWorkTypeDTO
    {
        public Guid WorkId { get; set; }
        public string Type { get; set; } = string.Empty;
    }

    public class SetWorkPriceDTO
    {
        public Guid WorkId { get; set; }
        public decimal Price { get; set; }
    }

    //------------------------------CHAPTER ZONE-----------------------------------//

    public class NewChapterDTO
	{
        public Guid WorkId { get; set; }
		public string Name { get; set; } = null!;
        public IFormFile? File { get; set; }
        public string? Type { get; set; }
        public string? Status { get; set; }
    }

    public class UpdateChapterDTO
    {
        public Guid ChapterId { get; set; }
        public Guid WorkId { get; set; }
        public string Name { get; set; } = null!;
        public IFormFile? ChapterFile { get; set; }
        public string Type { get; set; } = null!; 
        public string Status { get; set; } = null!;
    }
}

