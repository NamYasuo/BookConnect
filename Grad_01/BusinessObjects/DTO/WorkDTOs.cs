using System;
namespace BusinessObjects.DTO
{
	public class NewWorkDTO
	{
		public string Title { get; set; } = null!;
		public string Type { get; set; } = null!;
		public string Status { get; set; } = null!;
	}
	public class WorkIdTitleDTO
	{
		public Guid WorkId { get; set; }
		public string? Title { get; set; }
	}

}

