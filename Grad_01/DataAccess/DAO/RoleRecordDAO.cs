using System;
using BusinessObjects;
using BusinessObjects.Models.Authorization;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
	public class RoleRecordDAO
	{
		private readonly AppDbContext _context;
		public RoleRecordDAO()
		{
			_context = new AppDbContext();
		}

		public async Task<int> AddNewRoleRecordAsync(Guid userId, Guid roleId) 
		{
			await _context.RoleRecords.AddAsync(new RoleRecord
			{
				RoleRecordId = Guid.NewGuid(),
				RoleId = roleId,
				UserId = userId
			});
			return await _context.SaveChangesAsync();
		}

		public async Task<int> RemoveRoleRecordAsync(Guid userId,Guid roleId)
		{
			RoleRecord? roleRecord = await _context.RoleRecords.SingleOrDefaultAsync(r => r.RoleId == roleId && r.UserId == userId);
			if (roleRecord != null) _context.RoleRecords.Remove(roleRecord);
			return await _context.SaveChangesAsync();
		}
		
	}
}

