using APIs.Services.Interfaces;
using APIs.Utils.Paging;
using BusinessObjects.DTO.Trading;
using BusinessObjects.Models;
using BusinessObjects.Models.Creative;
using BusinessObjects.Models.E_com.Trading;
using BusinessObjects.Models.Trading;
using DataAccess.DAO;
using DataAccess.DAO.Creative;
using DataAccess.DAO.E_com;
using DataAccess.DAO.Trading;

namespace APIs.Services
{
    public class PostService : IPostService
    {
        //---------------------------------------------POST-------------------------------------------------------//

        public PagedList<Post> GetAllPost(PagingParams param)
        {
            return PagedList<Post>.ToPagedList(new PostDAO().GetAllPost().OrderBy(c => c.Title).AsQueryable(), param.PageNumber, param.PageSize);
        }

        public int AddNewPost(Post post) => new PostDAO().AddNewPost(post);

        public int DeletePostById(Guid postId) => new PostDAO().DeletePostById(postId);

        public Post GetPostById(Guid postId) => new PostDAO().GetPostById(postId);

        public int UpdatePost(Post post) => new PostDAO().UpdatePost(post);

        public string GetOldImgPath(Guid postId) => new PostDAO().GetOldImgPath(postId);

        public string GetOldVideoPath(Guid postId) => new PostDAO().GetOldVideoPath(postId);

        //---------------------------------------------COMMENT-------------------------------------------------------//

        public PagedList<Comment> GetCommentByPostId(Guid postId, PagingParams @params)
        {
            return PagedList<Comment>.ToPagedList(new CommentDAO().GetCommentByPostId(postId)?.OrderBy(ch => ch.Description).AsQueryable(), @params.PageNumber, @params.PageSize);
        }

        public int AddComment(Comment comment) => new CommentDAO().AddComment(comment);

        public int UpdateComment(Comment comment) => new CommentDAO().UpdateComment(comment);

        public int DeleteCommentById(Guid commentId) => new CommentDAO().DeleteCommentById(commentId);

        //---------------------------------------------POSTINTEREST-------------------------------------------------------//

        public int AddNewPostInterest(PostInterest postInterest) => new PostInterestDAO().AddNewPostInterest(postInterest);

        public int UpdatePostInterest(PostInterest postInterest) => new PostInterestDAO().UpdatePostInterest(postInterest);

        public int DeletePostInterestById(Guid postInterestId) => new PostInterestDAO().DeletePostInterestById(postInterestId);

        public PagedList<PostInterest> GetPostInterestByPostId(Guid postId, PagingParams @params)
        {
            return PagedList<PostInterest>.ToPagedList(new PostInterestDAO().GetPostInterestByPostId(postId)?.OrderBy(ch => ch.PostInterestId).AsQueryable(), @params.PageNumber, @params.PageSize);
        }

    }
}
