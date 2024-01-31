using System;
using BusinessObjects.DTO;
using BusinessObjects.Models.Creative;

namespace APIs.Services.Intefaces
{
	public interface IWorkService
	{
		public string AddNewWork(Work work);
		public List<WorkIdTitleDTO>? GetWorkIdTitleByAuthorId(Guid authorId);
	}
}

