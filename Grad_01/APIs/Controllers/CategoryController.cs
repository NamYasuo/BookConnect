using System;
using APIs.Services.Interfaces;
using BusinessObjects.DTO;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController: ControllerBase
	{
		private readonly ICategoryService _cateServices;
		public CategoryController(ICategoryService cateServices)
		{
			_cateServices = cateServices;
		}

		[HttpPost("add-category")]
		public IActionResult AddNewCate([FromForm] NewCateDTO dto)
		{
			try
			{
				if (ModelState.IsValid)
				{
					Category cate = new Category
					{
						CateId = Guid.NewGuid(),
						CateName = dto.CateName,
						ImageDir = "Handle later!",
						Description = dto.Description
					};
					int changes = _cateServices.AddCategory(cate);
					IActionResult result = (changes > 0) ? Ok("Successful!") : BadRequest("Add fail!");
					return result;
				} return BadRequest("Model invalid!");
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		[HttpGet("get-all-category")]
		public IActionResult GetAllCategory()
		{
			try
			{
				return Ok(_cateServices.GetAllCategory());
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		[HttpPut("update-category")]
		public IActionResult UpdateCategory([FromForm] NewCateDTO dto)
		{
			try
			{
				if (ModelState.IsValid)
				{
					Category cate = new Category
					{
						CateId = dto.CateId,
						CateName = dto.CateName,
						ImageDir = " handle later!",
						Description = dto.Description
					};

					int changes = _cateServices.UpdateCategory(cate);
					IActionResult result = (changes > 0) ? Ok("Successful!") : BadRequest("Update fail!");
					return result;
					
				} return BadRequest("Model invalid!");
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		[HttpGet("get-category-by-id")]
		public IActionResult GetCategoryById(Guid cateId)
		{
			try
			{
				return Ok(_cateServices.GetCategoryById(cateId));
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		[HttpDelete("delete-category")]
		public IActionResult DeleteCategory(Guid cateId)
		{
			try
			{
				int changes = _cateServices.DeleteCategory(cateId);
                IActionResult result = (changes > 0) ? Ok("Successful!") : BadRequest("Delete fail!");
				return result;
            }
            catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}

