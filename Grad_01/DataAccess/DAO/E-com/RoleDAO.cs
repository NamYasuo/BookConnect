using System;
using BusinessObjects;
using BusinessObjects.Models;

namespace DataAccess.DAO.Ecom
{
	public class RoleDAO
	{
		public Role GetRoleById(Guid roleId)
		{
			try
			{
				using(var context = new AppDbContext())
				{
					Role? record = context.Roles.Where(r => r.RoleId == roleId).SingleOrDefault();
					Role role = (record != null) ? record : new Role();
                    return role;
                }
            }
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public List<Role> GetAllRole()
		{
			try
			{
				using(var context = new AppDbContext())
				{
					return context.Roles.ToList();
				}
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public int DeleteRole(Guid roleId)
		{
			try
			{
				using(var context = new AppDbContext())
				{
					Role? record = context.Roles.Where(r => r.RoleId == roleId).SingleOrDefault();
					if(record != null)
					{
						context.Remove(record);
					}
					return context.SaveChanges();
				}
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}

