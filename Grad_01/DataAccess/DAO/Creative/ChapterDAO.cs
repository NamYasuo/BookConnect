using BusinessObjects;
using BusinessObjects.Models.Creative;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO.Creative
{
	public class ChapterDAO
	{
        private readonly AppDbContext _context;
        public ChapterDAO()
        {
            _context = new AppDbContext();
        }

        public async Task<Chapter?> GetChapterById(Guid chapterId)
        => await _context.Chapters.SingleOrDefaultAsync(c => c.ChapterId == chapterId);

        public async Task<int> AddChapterAsync(Chapter chapter)
        {
           await _context.Chapters.AddAsync(chapter);
           return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Chapter>> GetWorkChaptersAsync(Guid workId)
        => await _context.Chapters.Where(c => c.WorkId == workId).ToListAsync();
        

        public async Task<int> UpdateChapterAsync(Chapter chapter)
        {
           _context.Update(chapter);
           return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteChapterByIdAsync(Guid chapterId)
        {
              Chapter? chapter = await _context.Chapters.FirstOrDefaultAsync(c => c.ChapterId == chapterId);
              if (chapter != null) _context.Chapters.Remove(chapter);
              return await _context.SaveChangesAsync();
        }
    }
}

