﻿using System;
using BusinessObjects;
using BusinessObjects.DTO;
using BusinessObjects.DTO;
using BusinessObjects.Models;
using BusinessObjects.Models.Creative;

namespace DataAccess.DAO
{
	public class WorkDAO
	{
		//------------------------------GET-------------------------------------//
		//------------------------------GET-------------------------------------//
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
        public Work GetWorkById(Guid workId)
        {
            Work? work = new Work();
            Work? work = new Work();
            try
            {
                using (var context = new AppDbContext())
                {
                    work = context.Works.Where(b => b.WorkId == workId).FirstOrDefault();
                    work = context.Works.Where(b => b.WorkId == workId).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            if (work != null) return work;
            if (work != null) return work;
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
                    Work = context.Works.Where(b => b.Title == title).FirstOrDefault();
                    Work = context.Works.Where(b => b.Title == title).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            if (Work != null) return Work;
            else throw new NullReferenceException();
        }

        public List<WorkIdTitleDTO>? GetTitleAndIdByAuthorId(Guid authorId)
        {
            try
            {
                List<WorkIdTitleDTO>? results = new List<WorkIdTitleDTO>();
                using (var context = new AppDbContext())
                {
                    List<Work> works = context.Works.Where(w => w.AuthorId == authorId).ToList();
                    foreach (Work w in works)
                    {
                        results.Add(new WorkIdTitleDTO
                        {
                            Title = w.Title,
                            WorkId = w.WorkId
                        });
                    }
                }
                return results;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        //------------------------------ADD-------------------------------------//
        public List<WorkIdTitleDTO>? GetTitleAndIdByAuthorId(Guid authorId)
        {
            try
            {
                List<WorkIdTitleDTO>? results = new List<WorkIdTitleDTO>();
                using (var context = new AppDbContext())
                {
                    List<Work> works = context.Works.Where(w => w.AuthorId == authorId).ToList();
                    foreach (Work w in works)
                    {
                        results.Add(new WorkIdTitleDTO
                        {
                            Title = w.Title,
                            WorkId = w.WorkId
                        });
                    }
                }
                return results;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        //------------------------------ADD-------------------------------------//
        //Add new Work
        public string AddNewWork(Work work)
        {
            try
            {
                int result = 0;
                int result = 0;
                using (var context = new AppDbContext())
                {
                    //Check dupllicate work
                    if (!context.Works.Any(w => w.Title == work.Title))
                    //Check dupllicate work
                    if (!context.Works.Any(w => w.Title == work.Title))
                    {
                        context.Works.Add(work);
                        result = context.SaveChanges();
                        context.Works.Add(work);
                        result = context.SaveChanges();
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

        //-----------------------------UPDATE-----------------------------------//

        //-----------------------------UPDATE-----------------------------------//

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

        public int SetWorkPrice(Guid workId, decimal price)
        {
            try{
               using(var context = new AppDbContext())
                {
                    Work? work = context.Works.Where(w => w.WorkId == workId).SingleOrDefault();
                    if(work != null)
                    {
                        work.Price = price;
                    }
                    return context.SaveChanges();
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int SetWorkType(Guid workId, string type)
        {
            try{
                using(var context = new AppDbContext())
                {
                    Work? work = context.Works.Where(w => w.WorkId == workId).SingleOrDefault();
                    if(work != null)
                    {
                        work.Type = type;
                    }
                    return context.SaveChanges();
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        //-----------------------------DELETE-----------------------------------//
        public int SetWorkPrice(Guid workId, decimal price)
        {
            try{
               using(var context = new AppDbContext())
                {
                    Work? work = context.Works.Where(w => w.WorkId == workId).SingleOrDefault();
                    if(work != null)
                    {
                        work.Price = price;
                    }
                    return context.SaveChanges();
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int SetWorkType(Guid workId, string type)
        {
            try{
                using(var context = new AppDbContext())
                {
                    Work? work = context.Works.Where(w => w.WorkId == workId).SingleOrDefault();
                    if(work != null)
                    {
                        work.Type = type;
                    }
                    return context.SaveChanges();
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        //-----------------------------DELETE-----------------------------------//
        //Delete Work by id
        public int DeleteWorkById(Guid workId)
        public int DeleteWorkById(Guid workId)
        {
            try
            {
                int result = 0;
                int result = 0;
                using (var context = new AppDbContext())
                {
                    Work work = GetWorkById(workId);
                    if (work != null)
                    {
                        context.Works.Remove(work);
                        result = context.SaveChanges();
                        result = context.SaveChanges();
                    }
                }
                return result;
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        //-----------------------------CHECK-----------------------------------//
        public bool IsWorkCreator(Guid userId, Guid workId)
        {
            try
            {
                using(var context = new AppDbContext())
                {
                    Work? work = context.Works.Where(w => w.WorkId == workId).SingleOrDefault();
                    if(work != null)
                    {
                        return work.AuthorId == userId;
                    }
                    return false;
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}


