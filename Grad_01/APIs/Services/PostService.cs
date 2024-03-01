using APIs.Services.Intefaces;
using BusinessObjects.Models.Creative;
using BusinessObjects.Models.E_com.Trading;
using DataAccess.DAO;
using DataAccess.DAO.E_com;

namespace APIs.Services
{
    public class PostService: IPostService
    {
        public PostService()
        {
        }
        public void AddNewPost(Post post) => new PostDAO().AddNewPost(post);
    }
}
