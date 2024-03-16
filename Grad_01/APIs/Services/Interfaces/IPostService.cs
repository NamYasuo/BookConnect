using BusinessObjects.DTO;
using BusinessObjects.DTO.Trading;
using BusinessObjects.Models.E_com.Trading;

namespace APIs.Services.Interfaces
{
    public interface IPostService
    {
        public int AddNewPost(Post post);

        public int UpdatePost(Post Post);

        public void DeletePostById(Guid postId);

        public Post GetPostById(Guid postId);
        AddPostDTOs AddNewPost(Guid postId);
    }
}