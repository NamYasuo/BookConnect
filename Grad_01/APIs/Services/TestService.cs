using System;
using APIs.Services.Interfaces;
using BusinessObjects;
using BusinessObjects.Models;
using DataAccess.DAO;

namespace APIs.Services
{
	public class TestService: ITestService
	{
        private readonly TestingDAO _testDAO;
		public TestService()
		{
            _testDAO = new TestingDAO(new AppDbContext());
		}

        public async Task<int> AddNewTest(Testing data) => await _testDAO.AddNewTest(data);
       

        public Task<int> DeleteTest(Guid testId)
        {
            throw new NotImplementedException();
        }

        public Task<Testing> GetTestById(Guid testId)
        {
            throw new NotImplementedException();
        }
    }
}

