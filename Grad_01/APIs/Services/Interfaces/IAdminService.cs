using System;
namespace APIs.Services.Interfaces
{
	public interface IAdminService
	{
		public int SetIsBanned(bool choice, Guid userId);
	}
}

