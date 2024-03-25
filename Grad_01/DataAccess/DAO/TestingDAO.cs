using System;
using BusinessObjects;
using BusinessObjects.Models;
using BusinessObjects.Models.Creative;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
	public class TestingDAO
	{
		private readonly AppDbContext _context;

		public TestingDAO(AppDbContext context) {
			_context = context;
		}
		public async Task<int> AddNewTest(Testing data)
		{
			_context.Testings.Add(data);
			return await _context.SaveChangesAsync();
		}
		public async Task<Testing?> GetTestById(Guid testId)
        {
			return await _context.Testings.SingleOrDefaultAsync(t => t.TestId == testId);

        }
		public async Task<int> DeleteTesting(Guid testId)
		{
            Testing? testing = await _context.Testings.FirstOrDefaultAsync(c => c.TestId == testId);
            if (testing != null)
            {
                _context.Testings.Remove(testing);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
	}
}

