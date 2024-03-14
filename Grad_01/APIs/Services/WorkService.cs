using System;
using APIs.Services.Interfaces;
using APIs.Utils.Paging;
using BusinessObjects.DTO;
using BusinessObjects.Models.Creative;
using DataAccess.DAO;
using DataAccess.DAO.Creative;

namespace APIs.Services
{
	public class WorkService : IWorkService
	{
        //------------------------------WORK ZONE-----------------------------------//
        public string AddNewWork(Work work) => new WorkDAO().AddNewWork(work);

		public List<WorkIdTitleDTO>? GetWorkIdTitleByAuthorId(Guid authorId)
		=> new WorkDAO().GetTitleAndIdByAuthorId(authorId);

        public PagedList<Chapter> GetWorkChapters(Guid workId, PagingParams parameters)
		{
			return PagedList<Chapter>.ToPagedList(new ChapterDAO().GetWorkChapters(workId)?.OrderBy(ch => ch.Name).AsQueryable(), parameters.PageNumber, parameters.PageSize);
		}

		public WorkDetailsDTO GetWorkDetails(Guid workId)
		{
			Work work = new WorkDAO().GetWorkById(workId);
			WorkDetailsDTO result = new WorkDetailsDTO()
			{
				WorkId = workId,
				Title = work.Titile,
				Author = new AccountDAO().GetNameById(work.AuthorId),
				Type = work.Type,
				Status = work.Status,
				Price = work.Price.ToString(),
				CoverDir = work.CoverDir,
				BackgroundDir = work.BackgroundDir,
				Description = work.Description
			};
			return result;
		}

		public int DeleteWorkById(Guid workId) => new WorkDAO().DeleteWorkById(workId);


        //------------------------------CHAPTER ZONE-----------------------------------//

		public Chapter? GetChapterById(Guid chapterId) => new ChapterDAO().GetChapterById(chapterId);

        public int AddChapter(Chapter chapter) => new ChapterDAO().AddChapter(chapter);

        public int UpdateChapter(Chapter chapter) => new ChapterDAO().UpdateChapter(chapter);

		public int DeleteChapterById(Guid chapterId) => new ChapterDAO().DeleteChapterById(chapterId);

		public int SetWorkType(Guid workId, string type) => new WorkDAO().SetWorkType(workId, type);

		public int SetWorkPrice(Guid workId, decimal price) => new WorkDAO().SetWorkPrice(workId, price);
       
		public bool IsWorkOwner(Guid workId, Guid userId) => new WorkDAO().IsWorkCreator(userId, workId);

    }
}

