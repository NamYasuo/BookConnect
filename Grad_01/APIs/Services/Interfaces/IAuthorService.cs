using System;
namespace APIs.Services.Interfaces
{
	public interface IAuthorService
	{
		public bool IsWorkOwner(Guid workId, Guid userId);
		public bool IsAuthorValidated(Guid authorId);
	}
}

