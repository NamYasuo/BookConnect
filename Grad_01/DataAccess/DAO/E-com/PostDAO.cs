using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using BusinessObjects.Models;
using BusinessObjects.Models.E_com.Trading;
using Microsoft.Extensions.Caching.Memory;

namespace DataAccess.DAO.E_com
{
    public class PostDAO
    {
        //Get all post
        public List<Post> GetAllPost()
        {
            List<Post> postList = new List<Post>();
            try
            {
                using (var context = new AppDbContext())
                {
                    postList = context.Posts.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return postList;
        }

        //Get post by id
        public Post GetPostById(Guid postId)
        {
            Post? post = new Post();
            try
            {
                using (var context = new AppDbContext())
                {
                    post = context.Posts.Where(p => p.UserId == postId).FirstOrDefault();

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            if (post != null) return post;
            else throw new NullReferenceException();
        }
        //Add new post
        public void AddNewPost(Post post)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    context.Add(post);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        //Modify post
        public void UpdatePost(Post post)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    context.Posts.Update(post);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        //Delete post by id
        public void DeletePost(Guid postId)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    Post post = GetPostById(postId);
                    if (post != null)
                    {
                        context.Posts.Remove(post);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Post> GetPostListById(List<Guid> postIds)
        {
            List<Post> listPosts = new List<Post>();
            try
            {
                using (var context = new AppDbContext())
                {
                    foreach (Guid i in postIds)
                    {
                        listPosts.Add(context.Posts.Where(p => p.UserId == i).FirstOrDefault());
                    }
                    return listPosts;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
