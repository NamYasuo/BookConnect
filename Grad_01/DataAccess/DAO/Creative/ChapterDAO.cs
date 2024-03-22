using System;
using BusinessObjects;
using BusinessObjects.Models.Creative;

namespace DataAccess.DAO.Creative
{
	public class ChapterDAO
	{
        public Chapter? GetChapterById(Guid chapterId)
        {
            Chapter? result;
            try
            {
                using(var context = new AppDbContext())
                {
                    result = context.Chapters.Where(c => c.ChapterId == chapterId).FirstOrDefault();
                }
                return result;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int AddChapter(Chapter chapter)
        {
            try
            {
                int result = 0;
                using (var context = new AppDbContext())
                {
                    context.Chapters.Add(chapter);
                    result = context.SaveChanges();
                }
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<Chapter>? GetWorkChapters(Guid workId)
        {
            List<Chapter>? chapters = new List<Chapter>();
            try
            {
                using(var context = new AppDbContext())
                {
                    chapters = context.Chapters.Where(c => c.WorkId == workId).ToList();
                }
                return chapters;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int UpdateChapter(Chapter chapter)
        {
            int result = 0;
            try
            {
                using(var context = new AppDbContext())
                {
                    context.Update(chapter);
                    result = context.SaveChanges();
                }
                return result;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        public int DeleteChapterById(Guid chapterId) {
            int result = 0;
            try
            {
                using(var context = new AppDbContext())
                {
                    Chapter? index = context.Chapters.Where(c => c.ChapterId == chapterId).FirstOrDefault();
                    if (index != null)
                    {
                        context.Chapters.Remove(index);
                        result = context.SaveChanges();
                    }
                    return result;
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}

