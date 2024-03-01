using BusinessObjects.DTO;
using BusinessObjects.Models.Creative;
using BusinessObjects.Models.E_com.Trading;

namespace APIs.Services.Intefaces
{
    public interface IPostService
    {
        public void AddNewPost(Post post);
        //public List<PostIdTitleDTO>? GetPostIdTitleByUserId(Guid userId);
    }
}
