using BusinessObjects.DTO;
using BusinessObjects.Models.E_com.Trading;

namespace APIs.Services.Interfaces
{
    public interface IPostService
    {
        public void AddNewPost(Post post);

        public void UpdatePost(Post Post);

        public void DeletePost(Post post);

        public Post GetPost(int id);

        public Post GetPostById(int id);
    }
}