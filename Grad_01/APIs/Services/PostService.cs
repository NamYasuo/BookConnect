using APIs.Services.Interfaces;
using APIs.Utils.Paging;
using BusinessObjects.DTO.Trading;
using BusinessObjects.Models.Creative;
using BusinessObjects.Models.E_com.Trading;
using BusinessObjects.Models.Trading;
using DataAccess.DAO;
using DataAccess.DAO.Creative;
using DataAccess.DAO.E_com;

namespace APIs.Services
{
    public class PostService: IPostService
    {
        //---------------------------------------------POST-------------------------------------------------------//
        public int AddNewPost(Post post) => new PostDAO().AddNewPost(post);

        public AddPostDTOs AddNewPost(Guid postId)
        {
            throw new NotImplementedException();
        }

        public void DeletePostById(Guid postId) => new PostDAO().DeletePostById(postId);

        public Post GetPostById(Guid postId) => new PostDAO().GetPostById(postId);

        public int UpdatePost(Post post) => new PostDAO().UpdatePost(post);

        //---------------------------------------------COMMENT-------------------------------------------------------//
        public PagedList<Comment> GetCommentByPostId(Guid postId, PagingParams parameters)
        {
            return PagedList<Comment>.ToPagedList(new CommentDAO().GetCommentByPostID(postId)?.OrderBy(ch => ch.Description).AsQueryable(), parameters.PageNumber, parameters.PageSize);
        }

        public int UpdateComment(Comment comment) => new CommentDAO().UpdateComment(comment);
    }
}
