using APIs.DTO.Ecom;
using APIs.Services.Intefaces;
using BusinessObjects.Models.E_com.Trading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        [HttpPost("add-new-post")]
        public IActionResult AddNewPost([FromBody] Post data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return Ok();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            return BadRequest("Model state unvalid");
        }

    }
}
