using BusinessObjects;
using BusinessObjects.DTO.Trading;
using BusinessObjects.Models.Creative;
using BusinessObjects.Models.E_com.Trading;
using BusinessObjects.Models.Trading;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO.Trading
{
    public class CommentDAO
    {
        //Get comment by post id
        public IEnumerable<Comment>? GetCommentByPostId(Guid postId)
        {
            List<Comment>? comments = new List<Comment>();
            try
            {
                using (var context = new AppDbContext())
                {
                    comments = context.Comments.Where(c => c.PostId == postId).ToList();
                }
                return comments;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Add Comment
        public int AddComment(Comment comment)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    context.Comments.Add(comment);
                    return context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Update Comment
        public int UpdateComment(Comment comment)
        {
            int result = 0;
            try
            {
                using (var context = new AppDbContext())
                {
                    context.Update(comment);
                    result = context.SaveChanges();
                }
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Delete Comment by id
        public int DeleteCommentById(Guid commentId)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    Comment? comment = context.Comments.FirstOrDefault(c => c.CommentId == commentId);
                    if (comment != null)
                    {
                        context.Comments.Remove(comment);
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
