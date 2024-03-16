using APIs.Services.Interfaces;
using BusinessObjects.DTO.Trading;
using BusinessObjects.Models.Creative;
using BusinessObjects.Models.E_com.Trading;
using DataAccess.DAO;
using DataAccess.DAO.E_com;

namespace APIs.Services
{
    public class PostService: IPostService
    {
        public int AddNewPost(Post post) => new PostDAO().AddNewPost(post);

        public AddPostDTOs AddNewPost(Guid postId)
        {
            throw new NotImplementedException();
        }

        public void DeletePostById(Guid postId) => new PostDAO().DeletePostById(postId);

        public Post GetPostById(Guid postId) => new PostDAO().GetPostById(postId);

        public int UpdatePost(Post post) => new PostDAO().UpdatePost(post);
    }
}
