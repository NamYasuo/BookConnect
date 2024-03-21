using System;
using APIs.Services;
using APIs.Services.Interfaces;
using APIs.Utils.Paging;
using BusinessObjects.DTO;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController: ControllerBase
	{
		private readonly ICategoryService _cateServices;
		private readonly ICloudinaryService _clouinaryService;

		public CategoryController(ICategoryService cateServices, ICloudinaryService cloudinaryService)
		{
			_cateServices = cateServices;
			_clouinaryService = cloudinaryService;
		}

		[HttpPost("add-category")]
		public IActionResult AddNewCate([FromForm] NewCateDTO dto)
		{
			try
			{
				if (ModelState.IsValid)
				{
                    string imgUrl = string.Empty;
                    if (dto.CateImg != null)
					{
						CloudinaryResponseDTO cloudRsp
						= _clouinaryService.UploadImage(dto.CateImg, "Categories/Book");
						if (cloudRsp.StatusCode == 200 && cloudRsp.Data != null)
						{
							imgUrl = cloudRsp.Data;
						}
					}
                        Category cate = new Category
						{
						CateId = Guid.NewGuid(),
						CateName = dto.CateName,
						ImageDir = imgUrl,
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
		public IActionResult GetAllCategory([FromQuery] PagingParams @params)
        {
            try
			{
				var chapters = _cateServices.GetAllCategory(@params);


                if (chapters != null)
                {
                    var metadata = new
                    {
                        chapters.TotalCount,
                        chapters.PageSize,
                        chapters.CurrentPage,
                        chapters.TotalPages,
                        chapters.HasNext,
                        chapters.HasPrevious
                    };
                    Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                    return Ok(chapters);
                }
                else return BadRequest("No chapter!!!");
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
                    if (dto.CateId == null)
                    {
                        return BadRequest("Category id is cannot be null!");
                    }
                    string imgUrl = _cateServices.GetOldImgPath((Guid)dto.CateId);
                    if (dto.CateImg != null)
                    {
						string oldImg = imgUrl;

                        if (oldImg != "")
						{
							_clouinaryService.DeleteImage(oldImg, "Categories/Book");
						}
                        CloudinaryResponseDTO cloudRsp
                        = _clouinaryService.UploadImage(dto.CateImg, "Categories/Book");
                        if (cloudRsp.StatusCode == 200 && cloudRsp.Data != null)
                        {
                            imgUrl = cloudRsp.Data;
                        }
                    }
					Category cate = new Category
					{
						CateId = (Guid)dto.CateId,
						CateName = dto.CateName,
						ImageDir = imgUrl,
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
                string imgUrl = string.Empty;
                string oldImg = _cateServices.GetOldImgPath(cateId);

                    if (oldImg != "")
                    {
                        _clouinaryService.DeleteImage(oldImg, "Categories/Book");
                    }
                    int changes = _cateServices.DeleteCategory(cateId);
                IActionResult result = (changes > 0) ? Ok("Successful!") : BadRequest("Delete fail!");
				return result;
            }
            catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

        [HttpGet("search-category")]
        public IActionResult SearchCategory(string? inputString, [FromQuery] PagingParams param)
        {
			try
			{
				var cates = _cateServices.GetAllCategory(param);
				if (inputString != null && inputString != "")
				{
					cates = _cateServices.GetCategoryByName(inputString, param);
				}
                if (cates != null)
                {
                    var metadata = new
                    {
                        cates.TotalCount,
                        cates.PageSize,
                        cates.CurrentPage,
                        cates.TotalPages,
                        cates.HasNext,
                        cates.HasPrevious
                    };
                    Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                    return Ok(cates);
                }
                return Ok(cates);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
    }
}

