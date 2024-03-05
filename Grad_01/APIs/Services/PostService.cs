using APIs.Services.Intefaces;
using BusinessObjects.Models.Creative;
using BusinessObjects.Models.E_com.Trading;
using DataAccess.DAO;
using DataAccess.DAO.E_com;

namespace APIs.Services
{
    public class PostService: IPostService
    {
        public void AddNewPost(Post post) => new PostDAO().AddNewPost(post);

        public void DeletePost(Post post)
        {
            throw new NotImplementedException();
        }

        public Post GetPost(int id)
        {
            throw new NotImplementedException();
        }

        public Post GetPostById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdatePost(Post post) => new PostDAO().UpdatePost(post);
    }
}
