using System;
using BusinessObjects.Models;

namespace APIs.Services.Interfaces
{
	public interface ITestService
	{
		Task<int> AddNewTest(Testing data);
		Task<Testing> GetTestById(Guid testId);
		Task<int> DeleteTest(Guid testId);
	}
}

