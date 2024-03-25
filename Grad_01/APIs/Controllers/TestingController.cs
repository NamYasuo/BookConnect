using APIs.Services.Interfaces;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestingController: ControllerBase
	{
		private readonly ITestService _testService;
		public TestingController(ITestService testService)
		{
			_testService = testService;
		}

		[HttpPost("add-new-test")]
		public async Task<IActionResult> AddNewTest(Testing data)
		{
			int changes = await _testService.AddNewTest(data);

			IActionResult result = (changes > 0) ? Ok("Successful!") : Ok("No changes");
			return result;
		}
	}
}

