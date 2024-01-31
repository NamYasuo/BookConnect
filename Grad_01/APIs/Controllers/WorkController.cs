using System;
using APIs.Services.Intefaces;
using BusinessObjects.DTO;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkController: ControllerBase
	{
		private readonly IWorkService _workService;
		public WorkController(IWorkService workService)
		{
			_workService = workService;
		}

		[HttpGet("get-work-title-id-by-author")]
		public IActionResult GetWorkIdTitleByAuthor(Guid authorId)
		{
			try
			{
				List<WorkIdTitleDTO>? result = _workService.GetWorkIdTitleByAuthorId(authorId);
				if (result == null)
				{
					return Ok("No work found!!!");
				}
				else return Ok(result);

			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}

