using APIs.Utils.Paging;
using BusinessObjects.DTO;
using BusinessObjects.DTO.Trading;
using BusinessObjects.Models.E_com.Trading;
using BusinessObjects.Models.Trading;

namespace APIs.Services.Interfaces
{
    public interface IPostService
    {
        //---------------------------------------------POST-------------------------------------------------------//
        public int AddNewPost(Post post);

        public int UpdatePost(Post Post);

        public void DeletePostById(Guid postId);

        public Post GetPostById(Guid postId);
        AddPostDTOs AddNewPost(Guid postId);

        //---------------------------------------------COMMENT-------------------------------------------------------//
        public PagedList<Comment>? GetCommentByPostId(Guid postId, PagingParams @params);
        public int UpdateComment(Comment comment);
    }
}