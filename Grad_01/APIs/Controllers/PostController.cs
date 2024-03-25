using APIs.Services.Interfaces;
using APIs.Utils.Paging;
using BusinessObjects.DTO.Trading;
using BusinessObjects.Models.E_com.Trading;
using BusinessObjects.Models.Trading;
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
                            _cloudinaryService.DeleteImage(oldImgPath);
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
                //string? userPost = _accountService.GetUsernameById(UserId);
                string imgUrl = string.Empty;
                string oldImg = _postService.GetOldImgPath(postId);

                if (oldImg != "")
                {
                    _cloudinaryService.DeleteImage(oldImg);
                }
                int changes = _postService.DeletePostById(postId);
                IActionResult result = (changes > 0) ? Ok("Successful!") : BadRequest("Delete fail!");
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
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
                        CommenterId = comment.CommenterId,
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
                        CommenterId = comment.CommenterId,
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

        //---------------------------------------------POSTINTEREST-------------------------------------------------------//

        [HttpGet("get-post-interest-by-post-id")]
        public IActionResult GetPostInterestByPostId(Guid postId, [FromQuery] PagingParams @params)
        {
            try
            {
                var postInterest = _postService.GetPostInterestByPostId(postId, @params);


                if (postInterest != null)
                {
                    var metadata = new
                    {
                        postInterest.TotalCount,
                        postInterest.PageSize,
                        postInterest.CurrentPage,
                        postInterest.TotalPages,
                        postInterest.HasNext,
                        postInterest.HasPrevious
                    };
                    Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                    return Ok(postInterest);
                }
                else return BadRequest("No chapter!!!");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost("add-post-interest")]
        public IActionResult AddNewPostInterest([FromForm] AddPostInterestDTO postInterest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int result = _postService.AddNewPostInterest(new PostInterest()
                    {
                        PostInterestId = Guid.NewGuid(),
                        PostId = postInterest.PostId,
                        InteresterId = postInterest.InteresterId,
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

        [HttpPut("update-post-interest")]
        public IActionResult UpdatePostInterest([FromForm] UpdatePostInterestDTO postInterest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PostInterest updateData = new PostInterest
                    {
                        PostInterestId = postInterest.PostInterestId,
                        PostId = postInterest.PostId,
                    };
                    if (_postService.UpdatePostInterest(updateData) > 0)
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

        [HttpDelete("delete-post-interest")]
        public IActionResult DeletePostInterestById(Guid postInterestId)
        {
            try
            {
                _postService.DeletePostInterestById(postInterestId);
                var response = new
                {
                    StatusCode = 204,
                    Message = "Delete postInterest query was successful",
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    StatusCode = 500,
                    Message = "Delete postInterest query Internal Server Error",
                    Error = ex,
                };
                return StatusCode(500, response);
            }
        }
    }
}