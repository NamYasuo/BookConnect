using System;
using BusinessObjects;
using BusinessObjects.DTO;
using BusinessObjects.Models;
using BusinessObjects.Models.Creative;

namespace DataAccess.DAO
{
	public class WorkDAO
	{
		//Get all
		public List<Work> GetAllWork()
		{
			List<Work> works = new List<Work>();
			try
			{
				using (var context = new AppDbContext())
				{
					works = context.Works.ToList();
				}
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
			return works;
		}
        //Get work by id
        public Work GetWorkById(Guid workId)
        {
            Work? Work = new Work();
            try
            {
                using (var context = new AppDbContext())
                {
                    Work = context.Works.Where(b => b.WorkId == workId).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            if (Work != null) return Work;
            else throw new NullReferenceException();
        }

        //Get work by name
        public Work GetWorkByName(string title)
        {
            Work? Work = new Work();
            try
            {
                using (var context = new AppDbContext())
                {
                    Work = context.Works.Where(b => b.Titile == title).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            if (Work != null) return Work;
            else throw new NullReferenceException();
        }

        //Add new Work
        public string AddNewWork(Work work)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    //Dupllicate work
                    Work? duplicate = context.Works.Where(w => w.Titile == work.Titile).FirstOrDefault();
                    if(duplicate == null)
                    {
                        context.Add(work);
                        int result = context.SaveChanges();
                        if (result > 0) return "Successfully!";
                        else return "Fail to update to database";
                    }
                    return "Work existed!";
                   
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Modify Work
        public void UpdateWork(Work work)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    context.Works.Update(work);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Delete Work by id
        public void DeleteWork(Guid workId)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    Work work = GetWorkById(workId);
                    if (work != null)
                    {
                        context.Works.Remove(work);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<WorkIdTitleDTO>? GetTitleAndIdByAuthorId(Guid authorId)
        {
            try
            {
                List<WorkIdTitleDTO>? results = new List<WorkIdTitleDTO>();
                using(var context = new AppDbContext())
                {
                   List<Work> works = context.Works.Where(w => w.AuthorId == authorId).ToList();
                   foreach(Work w in works)
                    {
                        results.Add(new WorkIdTitleDTO
                        {
                            Title = w.Titile,
                            WorkId = w.WorkId
                        });
                    }
                }
                return results;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

    }
}


