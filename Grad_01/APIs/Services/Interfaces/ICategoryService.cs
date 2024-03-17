using System;
using APIs.Utils.Paging;
using BusinessObjects.Models;

namespace APIs.Services.Interfaces
{
	public interface ICategoryService
	{
		int AddCategory(Category cate);
		PagedList<Category> GetAllCategory(PagingParams param);
        int UpdateCategory(Category cate);
		int DeleteCategory(Guid cateId);
		Category GetCategoryById(Guid cateId);
		string GetOldImgPath(Guid cateId);

    }
}

