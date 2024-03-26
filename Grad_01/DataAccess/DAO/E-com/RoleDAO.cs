using BusinessObjects;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO.Ecom
{
    public class RoleDAO
    {
        private readonly AppDbContext _context;
        public RoleDAO()
        {
            _context = new AppDbContext();
        }
        /*--------------------------------------------System--------------------------------------------------------*/
        public async Task<Role?> GetRoleByIdAsync(Guid roleId) => await _context.Roles.SingleOrDefaultAsync(r => r.RoleId == roleId);

        public async Task<List<Role>> GetAllRoleAsync() => await _context.Roles.ToListAsync();

        public async Task<int> DeleteRoleAsync(Guid roleId)
        {
            Role? record = await _context.Roles.SingleOrDefaultAsync(r => r.RoleId == roleId);
            if (record != null)
            {
              _context.Remove(record);
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<Role?> GetRolesDetails(string roleName)
        => await _context.Roles.SingleOrDefaultAsync(r => r.RoleName.Equals(roleName));

        public async Task<Role?> GetRoleById(Guid roleId) => await _context.Roles.SingleOrDefaultAsync(r => r.RoleId == roleId);

        public async Task<int> AddNewRole(Role role)
        {
            await _context.Roles.AddAsync(role);
            return await _context.SaveChangesAsync();
        }
        /*--------------------------------------------User--------------------------------------------------------*/
        public async Task<Dictionary<Guid, string>> GetAllUserRolesAsync(Guid userId)
        {
            var query = from record in _context.RoleRecords
                        join role in _context.Roles on record.RoleId equals role.RoleId
                        where record.UserId.Equals(userId)
                        select new { role.RoleName, role.RoleId };

            Dictionary<Guid, string> results = new Dictionary<Guid, string>();

            await query.ForEachAsync(v =>
            {
                results.Add(v.RoleId, v.RoleName);

            });
            return results;
        }
    }
}

