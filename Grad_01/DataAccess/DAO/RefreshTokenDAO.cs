using System;
using BusinessObjects;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
	public class RefreshTokenDAO
	{
		private readonly AppDbContext _context;
		public RefreshTokenDAO()
		{
			_context = new AppDbContext();
		}
		public async Task<int> AddRefreshTokenAsync(RefreshToken data)
		{
			await _context.RefreshTokens.AddAsync(data);
			return await _context.SaveChangesAsync();
		}

		public async Task<RefreshToken?> ValidateRefreshTokenAsync(string token) => await _context.RefreshTokens.SingleOrDefaultAsync(t => t.Token == token);


	}
}

