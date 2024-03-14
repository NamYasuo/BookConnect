using System;
using APIs.Utils.Paging;
using BusinessObjects.DTO;
using BusinessObjects.Models.Creative;

namespace APIs.Services.Interfaces
{
	public interface IWorkService
	{
		//Work
		//Common CRUD
		public string AddNewWork(Work work);
		public List<WorkIdTitleDTO>? GetWorkIdTitleByAuthorId(Guid authorId);
		public PagedList<Chapter>? GetWorkChapters(Guid workId, PagingParams @params);
		public WorkDetailsDTO GetWorkDetails(Guid workId);
        public int DeleteWorkById(Guid workId);
		//After validate the account
		public int SetWorkType(Guid workId, string type);
		public int SetWorkPrice(Guid workId, decimal price);

        //Chapter
        public Chapter? GetChapterById(Guid chapterId);
        public int AddChapter(Chapter chapter);
		public int UpdateChapter(Chapter chapter);
		public int DeleteChapterById(Guid chapterId);

    }
}

