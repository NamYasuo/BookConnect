using System;
using APIs.Services.Intefaces;
using BusinessObjects.DTO;
using BusinessObjects.Models.Creative;
using DataAccess.DAO;

namespace APIs.Services
{
	public class WorkService: IWorkService
	{
		public WorkService()
		{
		}

		public string AddNewWork(Work work) => new WorkDAO().AddNewWork(work);

		public List<WorkIdTitleDTO?> GetWorkIdTitleByAuthorId(Guid authorId)
		=> new WorkDAO().GetTitleAndIdByAuthorId(authorId);

    }
}

