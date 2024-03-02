using System;
using APIs.Utils.Paging;
using BusinessObjects.DTO;
using BusinessObjects.Models.Creative;

namespace APIs.Services.Interfaces
{
	public interface IWorkService
	{
		//Work
		public string AddNewWork(Work work);
		public List<WorkIdTitleDTO>? GetWorkIdTitleByAuthorId(Guid authorId);
		public PagedList<Chapter>? GetWorkChapters(Guid workId, PagingParams @params);
		public WorkDetailsDTO GetWorkDetails(Guid workId);
        public int DeleteWorkById(Guid workId);

        //Chapter
        public Chapter? GetChapterById(Guid chapterId);
        public int AddChapter(Chapter chapter);
		public int UpdateChapter(Chapter chapter);
		public int DeleteChapterById(Guid chapterId);

    }
}

