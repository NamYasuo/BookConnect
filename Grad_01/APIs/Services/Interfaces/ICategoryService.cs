using System;
using BusinessObjects.Models;

namespace APIs.Services.Interfaces
{
	public interface ICategoryService
	{
		int AddCategory(Category cate);
		List<Category> GetAllCategory();
		int UpdateCategory(Category cate);
		int DeleteCategory(Guid cateId);
		Category GetCategoryById(Guid cateId);
	}
}

