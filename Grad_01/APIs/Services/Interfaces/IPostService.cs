using APIs.Utils.Paging;
using BusinessObjects.DTO;
using BusinessObjects.DTO.Trading;
using BusinessObjects.Models;
using BusinessObjects.Models.Creative;
using BusinessObjects.Models.E_com.Trading;
using BusinessObjects.Models.Trading;

namespace APIs.Services.Interfaces
{
    public interface IPostService
    {
        //---------------------------------------------POST-------------------------------------------------------//

        PagedList<Post> GetAllPost(PagingParams param);

        public Post? GetPostById(Guid postId);

        public int AddNewPost(Post post);

        public int UpdatePost(Post Post);

        public int DeletePostById(Guid postId);

        string GetOldImgPath(Guid postId);

        string GetOldVideoPath(Guid postId);

        //---------------------------------------------COMMENT-------------------------------------------------------//

        public PagedList<Comment> GetCommentByPostId(Guid postId, PagingParams @params);

        public int AddComment(Comment comment);

        public int UpdateComment(Comment comment);

        public int DeleteCommentById(Guid commentId);

        //---------------------------------------------POSTINTEREST-------------------------------------------------------//

        public PagedList<PostInterest> GetPostInterestByPostId(Guid postId, PagingParams @params);

        public int AddNewPostInterest(PostInterest postInterest);

        public int UpdatePostInterest(PostInterest PostInterest);

        public int DeletePostInterestById(Guid postInterestId);
    }
}