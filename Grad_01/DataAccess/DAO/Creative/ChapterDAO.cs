using System;
using BusinessObjects;
using BusinessObjects.Models.Creative;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO.Creative
{
	public class ChapterDAO
	{
        public async Task<Chapter?> GetChapterById(Guid chapterId)
        {
            using (var context = new AppDbContext())
            {
                return await context.Chapters.SingleOrDefaultAsync(c => c.ChapterId == chapterId);
            }
        }

        public async Task<int> AddChapterAsync(Chapter chapter)
        {
            using (var context = new AppDbContext())
            {
                context.Chapters.Add(chapter);
                return await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Chapter>> GetWorkChaptersAsync(Guid workId)
        {
            using (var context = new AppDbContext())
            {
                return await context.Chapters.Where(c => c.WorkId == workId).ToListAsync();
            }
        }

        public async Task<int> UpdateChapterAsync(Chapter chapter)
        {
            using (var context = new AppDbContext())
            {
                context.Update(chapter);
                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteChapterByIdAsync(Guid chapterId)
        {
            using (var context = new AppDbContext())
            {
                Chapter? chapter = await context.Chapters.FirstOrDefaultAsync(c => c.ChapterId == chapterId);
                if (chapter != null)
                {
                    context.Chapters.Remove(chapter);
                    return await context.SaveChangesAsync();
                }
                return 0;
            }
        }



    }
}

