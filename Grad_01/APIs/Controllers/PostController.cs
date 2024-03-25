using APIs.Repositories.Interfaces;
using APIs.Services;
using APIs.Services.Interfaces;
using APIs.Utils.Base;
using APIs.Utils.Paging;
using BusinessObjects.DTO;
using BusinessObjects.DTO.Trading;
using BusinessObjects.Models.Creative;
using BusinessObjects.Models.E_com.Trading;
using BusinessObjects.Models.Trading;
using DataAccess.DAO.E_com;
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
        private readonly ICloudinaryService _cloudinaryService;
        public PostController(IPostService postService, ICloudinaryService cloudinaryService, IAccountService accountService)
        {
            _cloudinaryService = cloudinaryService;
            _postService = postService;
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
                var saveProductResult = _cloudinaryService.UploadImage(dto.ProductImgs, "Post/" + userPost + "/" + dto.Title + "/Image");
                if (saveProductResult.StatusCode != 200)
                {
                    return BadRequest(saveProductResult.Message);
                }
                productDir = saveProductResult.Data;
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
                        ImgDir = productDir,
                        Title = dto.Title,
                        Content = dto.Content,
                        CreatedAt = DateTime.Now,
                    });
                    if (result > 0)
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
        public IActionResult UpdatePost([FromForm] UpdatePostDTOs dto)
        {
            try
            {
                string? userPost = _accountService.GetUsernameById(dto.UserId);
                string newImgPath = "";
                if (ModelState.IsValid)
                {
                    if (dto.ProductImgs != null)
                    {
                        string? oldImgPath = _postService.GetPostById(dto.PostId)?.ImgDir;

                        if (oldImgPath != null)
                        {
                            _cloudinaryService.DeleteImage(oldImgPath, "Post/" + userPost + "/" + dto.Title + "/Image");
                        }
                        var cloudResponse = _cloudinaryService.UploadImage(dto.ProductImgs, "Post/" + dto.Title + "/Image");
                        if (cloudResponse.StatusCode != 200)
                        {
                            return BadRequest(cloudResponse.Message);
                        }
                        newImgPath = cloudResponse.Data;
                    }
                    Post updateData = new Post
                    {
                        PostId = dto.PostId,
                        UserId = dto.UserId,
                        AuthorName = dto.AuthorName,
                        ImgDir = newImgPath,
                        Title = dto.Title,
                        Content = dto.Content,
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
        public IActionResult DeletePostById(Guid postId)
        {
            try
            {
                _postService.DeletePostById(postId);
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

        [HttpPost("add-comment")]
        public IActionResult AddComment([FromForm] AddCommentDTO comment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int result = _postService.AddComment(new Comment()
                    {
                        CommentId = Guid.NewGuid(),
                        PostId = comment.PostId,
                        Description = comment.Description,
                        Created = DateTime.Now
                    });
                    if (result > 0)
                    {
                        return Ok();
                    }
                    return BadRequest("Add false");
                }
                return BadRequest("Comment Invalid");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPut("Update-Comment")]
        public IActionResult UpdateComment([FromForm] UpdateCommentDTO comment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Comment updateData = new Comment
                    {
                        CommentId = comment.CommentId,
                        PostId = comment.PostId,
                        Description = comment.Description,
                        Created = DateTime.Now
                    };
                    if (_postService.UpdateComment(updateData) > 0)
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

        [HttpDelete("delete-comment")]
        public IActionResult DeleteCommentById(Guid commentId)
        {
            try
            {
                _postService.DeleteCommentById(commentId);
                var response = new
                {
                    StatusCode = 204,
                    Message = "Delete comment query was successful",
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    StatusCode = 500,
                    Message = "Delete comment query Internal Server Error",
                    Error = ex,
                };
                return StatusCode(500, response);
            }
        }
    }
}