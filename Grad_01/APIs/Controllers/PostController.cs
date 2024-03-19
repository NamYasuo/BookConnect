using APIs.Repositories.Interfaces;
using APIs.Services;
using APIs.Services.Interfaces;
using APIs.Utils.Base;
using APIs.Utils.Paging;
using BusinessObjects.DTO;
using BusinessObjects.DTO.Trading;
using BusinessObjects.Models.Creative;
using BusinessObjects.Models.E_com.Trading;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IAccountService _accountService;
        private FileSaver _fileSaver;
        public PostController(IPostService postService, IWebHostEnvironment env, IAccountService accountService) {
            _postService = postService;
            _fileSaver = new FileSaver();
            _accountService = accountService;
        }
        //---------------------------------------------POST-------------------------------------------------------//
        [HttpPost("add-new-post")]
        public IActionResult AddNewPost([FromForm] AddPostDTOs dto)
        {
            string? userPost = _accountService.GetUsernameById(dto.UserId);
            string productDir = "";
            if (dto.ProductImgs != null)
            {
                productDir = _fileSaver.FileSaveAsync(dto.ProductImgs, "src/assets/FileSystem/" + "post/" + userPost + "/" + dto.Title + "/Cover");// dduowngf link + "post/" + authorName + "/"
            }
            try
            {
                if (ModelState.IsValid)
                {
                    int result = _postService.AddNewPost(new Post()
                    {
                        UserId = dto.UserId,
                        PostId = Guid.NewGuid(),
                        AuthorName = dto.AuthorName,
                        Title = dto.Title,
                        Content = dto.Content,
                        CreatedAt = DateTime.Now,
                    });
                    if(result > 0)
                    { 
                        return Ok();
                    }
                    return BadRequest("Add false");
                }
                return BadRequest("Post Invalid");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPut("update posts")]
        public IActionResult UpdatePost([FromForm] UpdatePostDTOs post)
        {
            try
            {
                string newImgPath = "";
                if (ModelState.IsValid)
                {
                    if (post.ProductImgs != null)
                    {
                        string? oldImgPath = _postService.GetPostById(post.PostId)?.ImgDir;

                        if (oldImgPath != null)
                        {
                            _fileSaver.FileDelete(oldImgPath);
                        }
                        newImgPath = _fileSaver.FileSaveAsync(post.ProductImgs, "src/assets/FileSystem/" + "post/" + oldImgPath + "/" + post.Title + "/Cover");//"src/assets/FileSystem/" + "post/" + authorName + "/" + post.Title + "/Cover"
                    }
                    Post updateData = new Post
                    {
                        PostId = Guid.NewGuid(),
                        AuthorName = post.AuthorName,
                        Title = post.Title,
                        Content = post.Content,
                        CreatedAt = DateTime.Now,
                    };
                    if (_postService.UpdatePost(updateData) > 0)
                    {
                        return Ok("Successful");
                    }
                    return BadRequest("Update fail");
                }
                return BadRequest("Model state invalid");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpDelete("delete-post")]
        public IActionResult DeletePostById(Guid PostId)
        {
            try
            {
                _postService.DeletePostById(PostId);
                var response = new
                {
                    StatusCode = 204,
                    Message = "Delete post query was successful",
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    StatusCode = 500,
                    Message = "Delete post query Internal Server Error",
                    Error = ex,
                };
                return StatusCode(500, response);
            }
        }

        //---------------------------------------------COMMENT-------------------------------------------------------//
        [HttpGet("get-comment-by-post-id")]
        public IActionResult GetCommentByPostId(Guid postId, [FromQuery] PagingParams @params)
        {
            try
            {
                var comment = _postService.GetCommentByPostId(postId, @params);


                if (comment != null)
                {
                    var metadata = new
                    {
                        comment.TotalCount,
                        comment.PageSize,
                        comment.CurrentPage,
                        comment.TotalPages,
                        comment.HasNext,
                        comment.HasPrevious
                    };
                    Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                    return Ok(comment);
                }
                else return BadRequest("No chapter!!!");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /*
         * [HttpPost("add-comment")]
public IActionResult AddComment(Guid postId, CommentDTO comment)
{
    try
    {
        var post = _postService.GetPostById(postId);
        if (post == null)
        {
            return NotFound("Post not found");
        }

        // Thực hiện các xử lý kiểm tra và lưu trữ comment vào cơ sở dữ liệu
        // Sử dụng _postService hoặc một dịch vụ tương ứng để xử lý thêm comment

        // Trả về phản hồi thành công nếu comment được lưu thành công
        return Ok("Comment added successfully!");
    }
    catch (Exception e)
    {
        throw new Exception(e.Message);
    }
}
        */
    }
}