using BusinessObjects;
using BusinessObjects.Models;

namespace DataAccess.DAO
{
	public class CategoryDAO
	{
		public int AddCategory(Category cate)
		{
			try
			{
				using(var context = new AppDbContext())
				{
					context.Categories.Add(cate);
					return context.SaveChanges();
				}
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public List<Category> GetAllCategory()
		{
			try
			{
				List<Category> result = new List<Category>();
				using(var context = new AppDbContext())
				{
					if (context.Categories.Any())
					{
						result = context.Categories.ToList();
					}
					return result;
				}
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public int UpdateCategory(Category cate)
		{
            try
            {
                using (var context = new AppDbContext())
                {
                    context.Categories.Update(cate);
                    return context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int DeleteCategoryById(Guid cateId)
        {
            try
            {
                using (var context = new AppDbContext())
                {
					Category? cate = context.Categories.Where(c => c.CateId == cateId).SingleOrDefault();

                    if (cate != null)
					{
                        context.Categories.Remove(cate);
                    }
                    return context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

		public Category GetCategoryById(Guid cateId)
		{
            try
            {
                using (var context = new AppDbContext())
                {
                    Category? result = context.Categories.Where(c => c.CateId == cateId).SingleOrDefault();
                    if (result != null)
                    {
                        return result;
                    }
                    return new Category();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string GetOldImgPath(Guid cateId)
        {
            try
            {
                using(var context = new AppDbContext())
                {
                    Category? cate = context.Categories.Where(c => c.CateId == cateId).SingleOrDefault();
                    string result = (cate != null && cate.ImageDir != null) ?
                        cate.ImageDir : "";
                    return result;
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

