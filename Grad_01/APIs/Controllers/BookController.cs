//using System;
//using APIs.Repositories.Interfaces;
//using BusinessObjects.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace APIs.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class BookController: ControllerBase
//	{
//		private readonly IBookRepository bookRepo;

//		public BookController(IBookRepository bookRepo)
//		{
//			this.bookRepo = bookRepo;
//		}

//		//Get all book 
//		[HttpGet("GetAll"), Authorize(Roles = "Nerd")]
//		public IActionResult GetAll()
//		{
//			try
//			{
//                return Ok(bookRepo.GetAllBook());
//            }
//			catch(Exception e)
//			{
//				return BadRequest(e.Message);
//			}

//        }

//		//Add book
//		[HttpPost("AddNew")]
//		public IActionResult AddNewBook([FromBody] Book book)
//		{
//			try
//			{
//				bookRepo.AddNewBook(book);
//                return Ok();
//			}
//			catch(Exception e)
//			{
//				return BadRequest(e.Message);
//			}
//		}
//		//Modify book

//		//Delete book
//	}
//}

