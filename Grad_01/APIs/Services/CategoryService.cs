using System;
using APIs.Services.Interfaces;
using BusinessObjects.Models;
using DataAccess.DAO;

namespace APIs.Services
{
    public class CategoryService : ICategoryService
    {
        public int AddCategory(Category cate) => new CategoryDAO().AddCategory(cate);

        public int DeleteCategory(Guid cateId) => new CategoryDAO().DeleteCategoryById(cateId);
        

        public List<Category> GetAllCategory() => new CategoryDAO().GetAllCategory();

        public int UpdateCategory(Category cate) => new CategoryDAO().UpdateCategory(cate);

        public Category GetCategoryById(Guid cateId) => new CategoryDAO().GetCategoryById(cateId);
    }
}

