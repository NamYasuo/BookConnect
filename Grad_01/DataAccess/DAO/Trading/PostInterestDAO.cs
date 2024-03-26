using BusinessObjects.Models.Trading;
using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models.E_com.Trading;
using BusinessObjects.Models.Creative;

namespace DataAccess.DAO.Trading
{
    public class PostInterestDAO
    {

        //Get postInterest by post id
        public IEnumerable<PostInterest>? GetPostInterestByPostId(Guid postId)
        {
            List<PostInterest>? postInterests = new List<PostInterest>();
            try
            {
                using (var context = new AppDbContext())
                {
                    postInterests = context.PostInterests.Where(c => c.PostId == postId).ToList();
                }
                return postInterests;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Add Interest
        public int AddNewPostInterest(PostInterest postInterest)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    context.PostInterests.Add(postInterest);
                    return context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Update PostInterest
        public int UpdatePostInterest(PostInterest postInterest)
        {
            int result = 0;
            try
            {
                using (var context = new AppDbContext())
                {
                    context.Update(postInterest);
                    result = context.SaveChanges();
                }
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Delete PostInterest by id
        public int DeletePostInterestById(Guid postInterestId)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    PostInterest? postInterest = context.PostInterests.FirstOrDefault(c => c.PostInterestId == postInterestId);
                    if (postInterest != null)
                    {
                        context.PostInterests.Remove(postInterest);
                    }
                    return context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
