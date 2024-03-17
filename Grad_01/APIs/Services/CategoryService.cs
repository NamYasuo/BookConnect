using System;
using APIs.Services.Interfaces;
using APIs.Utils.Paging;
using BusinessObjects.Models;
using BusinessObjects.Models.Creative;
using CloudinaryDotNet.Actions;
using DataAccess.DAO;
using DataAccess.DAO.Creative;

namespace APIs.Services
{
    public class CategoryService : ICategoryService
    {
        public int AddCategory(Category cate) => new CategoryDAO().AddCategory(cate);

        public int DeleteCategory(Guid cateId) => new CategoryDAO().DeleteCategoryById(cateId);
        

        public PagedList<Category> GetAllCategory(PagingParams param )
        {
            return PagedList<Category>.ToPagedList(new CategoryDAO().GetAllCategory().OrderBy(c => c.CateName).AsQueryable(), param.PageNumber, param.PageSize);
        }

        public int UpdateCategory(Category cate) => new CategoryDAO().UpdateCategory(cate);

        public Category GetCategoryById(Guid cateId) => new CategoryDAO().GetCategoryById(cateId);

        public string GetOldImgPath(Guid cateId) => new CategoryDAO().GetOldImgPath(cateId);
        
    }
}

