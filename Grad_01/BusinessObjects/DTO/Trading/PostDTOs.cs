using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.Trading
{
//-------------------------------------------------POST-----------------------------------------------------//
    public class AddPostDTOs
    {
        public Guid UserId { get; set; }
        public string? AuthorName { get; set; } = null!;
        public IFormFile? ProductImgs { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        //public List<AddPostDTOs> CateId { get; set; } = null!;
    }

    public class UpdatePostDTOs
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public string? AuthorName { get; set; } = null!;
        public IFormFile? ProductImgs { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        //public List<AddPostDTOs> CateId { get; set; } = null!;
    }

    //-------------------------------------------------COMMENT-----------------------------------------------------//
    public class GetCommentByPostIdDTO
    {
        public Guid CommentId { get; set; }
        public Guid PostId { get; set; }
        public Guid CommenterId { get; set; }
        public string Description { get; set; }
    }

    public class AddCommentDTO
    {
        public Guid PostId { get; set; }
        public Guid CommenterId { get; set; }
        public string Description { get; set; }
    }

    public class UpdateCommentDTO
    {
        public Guid CommentId { get; set; }
        public Guid PostId { get; set; }
        public Guid CommenterId { get; set; }
        public string Description { get; set; }
    }

    public class DeleteCommentDTO
    {
        public Guid CommentId { get; set; }
        public Guid PostId { get; set; }
        public Guid CommenterId { get; set; }
        public string Description { get; set; }
    }
    //-------------------------------------------------POSTINTEREST-----------------------------------------------------//
    public class AddPostInterestDTO
    {
        public Guid PostId { get; set; }
        public Guid InteresterId { get; set; }
    }

    public class UpdatePostInterestDTO
    {
        public Guid PostId { get; set; }
        public Guid PostInterestId { get; set; }
        public Guid InteresterId { get; set; }
    }

    public class DeletePostInterestDTO
    {
        public Guid InteresterId { get; set; }
        public Guid PostId { get; set;}
    }

}