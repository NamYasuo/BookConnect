﻿using System;
using System.Text.RegularExpressions;
using APIs.Services.Interfaces;
using APIs.Utils.Base;
using APIs.Utils.Paging;
using BusinessObjects.DTO;
using BusinessObjects.Models;
using BusinessObjects.Models.Creative;
using DataAccess.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkController: ControllerBase
	{
		private readonly IWorkService _workService;
		private readonly IAccountService _accountService;
		private FileSaver _fileSaver;
		public WorkController(IWorkService workService, IWebHostEnvironment env, IAccountService accountService)
		{
			_workService = workService;
			_fileSaver = new FileSaver();
			_accountService = accountService;
		}


        //------------------------------WORK ZONE-----------------------------------//
        [HttpPost("add-new-work")]
		public IActionResult AddNewWork([FromForm] NewWorkDTO dto)
		{
			try
			{
				if (ModelState.IsValid)
				{

                 
                    string? authorName = _accountService.GetUsernameById(dto.AuthorId);
                    string coverDir = "";
					string backgroundDir = "";
					if (dto.Cover != null)
					{
						coverDir = _fileSaver.FileSaveAsync(dto.Cover, "src/assets/FileSystem/" + authorName + "/" + dto.Title + "/Cover");
					}
					if (dto.Background != null)
					{
						backgroundDir = _fileSaver.FileSaveAsync(dto.Background, "src/assets/FileSystem/" + authorName + "/" + dto.Title + "/Background");
					}

					string result = _workService.AddNewWork(new Work()
                    {
                        WorkId = Guid.NewGuid(),
						Titile = dto.Title,
						AuthorId = dto.AuthorId,
						Type = dto.Type,
						Status = dto.Status,
						CoverDir = coverDir,
						BackgroundDir = backgroundDir,
						Description = dto.Description

                    });
					return Ok(result);
				}
				return BadRequest("Model invalid");
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		[HttpGet("get-work-title-id-by-author")]
		public IActionResult GetWorkIdTitleByAuthor(Guid authorId)
		{
			try
			{
				List<WorkIdTitleDTO>? result = _workService.GetWorkIdTitleByAuthorId(authorId);
				if (result == null)
				{
					return Ok("No work found!!!");
				}
				else return Ok(result);

			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

        [HttpGet("get-work-details")]
        public IActionResult GetWorkDetails(Guid workId)
        {
            try
            {
                var result = _workService.GetWorkDetails(workId);
                if (result != null) return Ok(result);
                else return BadRequest("Work not found!!!");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet("get-work-chapters")]
        public IActionResult GetWorkChapters(Guid workId, [FromQuery] PagingParams @params)
        {
            try
            {
                var chapters = _workService.GetWorkChapters(workId, @params);


                if (chapters != null)
                {
                    var metadata = new
                    {
                        chapters.TotalCount,
                        chapters.PageSize,
                        chapters.CurrentPage,
                        chapters.TotalPages,
                        chapters.HasNext,
                        chapters.HasPrevious
                    };
                    Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                    return Ok(chapters);
                }
                else return BadRequest("No chapter!!!");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

		[HttpDelete("delete-work")]
		public IActionResult DeleteWorkById(Guid workId)
		{
			try
			{
				WorkDetailsDTO work = _workService.GetWorkDetails(workId);
				if(work.BackgroundDir != null && work.BackgroundDir != "")
				{
					_fileSaver.FileDelete(work.BackgroundDir);
				}
                if (work.CoverDir != null && work.CoverDir != "")
                {
                    _fileSaver.FileDelete(work.CoverDir);
                }
				int result = _workService.DeleteWorkById(workId);
				if (result > 0) return Ok("Successful!");
				else return BadRequest("Fail to delete!");
            }
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		//------------------------------CHAPTER ZONE-----------------------------------//

        [HttpPost("chapters/add-chapter")]
        public IActionResult AddChapter([FromForm] NewChapterDTO chapter)
        {
            try
            {
                int result = 0;
                if (ModelState.IsValid)
                {
                    if (chapter.File != null)
                    {
						string workName = _workService.GetWorkDetails(chapter.WorkId).Title;
						string? authorName = _accountService.GetUsernameById(chapter.WorkId);
                        string? dir = _fileSaver.FileSaveAsync(chapter.File, "src/assets/FileSystem/" + authorName + "/" + workName + "/" + chapter.Name);
                        if (dir != null)
                        {
                            result = _workService.AddChapter(new Chapter
                            {
                                WorkId = chapter.WorkId,
                                ChapterId = Guid.NewGuid(),
                                Name = chapter.Name,
                                Directory = dir,
                                Type = chapter.Type,
                                Status = chapter.Status,
                            });
                            //modify to async
                            if (result == 1) return Ok("Successfull");
                        }
                        else return BadRequest("Fail to save file");
                        return Ok(dir);
                    }
                    return BadRequest("File is null");
                }
                return BadRequest("Model invalid");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

		[HttpPut("chapters/update-chapter")]
		public IActionResult UpdateChapter([FromForm] UpdateChapterDTO chapter)
		{
			try
			{
				string newImgPath = "";
				if (ModelState.IsValid)
				{
					if(chapter.ChapterFile != null)
					{
						string? oldImgPath = _workService.GetChapterById(chapter.ChapterId)?.Directory;

                        if (oldImgPath != null)
						{
							 _fileSaver.FileDelete(oldImgPath);
						}
						newImgPath = _fileSaver.FileSaveAsync(chapter.ChapterFile, "src/assets/FileSystem/" + "Author01" + "/" + "Work_01" + "/" + "Chapter");
					}
                    Chapter updateData = new Chapter
                    {
                        WorkId = chapter.WorkId,
                        ChapterId = chapter.ChapterId,
                        Directory = newImgPath,
                        Name = chapter.Name,
                        Type = chapter.Type,
                        Status = chapter.Status,
                    };
                    if (_workService.UpdateChapter(updateData) > 0)
                    {
                        return Ok("Successful");
                    }
                    return BadRequest("Update fail");
                } return BadRequest("Model state invalid");
            }
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		[HttpDelete("chapters/delete-chapter")]
		public IActionResult DeleteChapter(Guid chapterId)
		{
			try
			{
				Chapter? chapter = _workService.GetChapterById(chapterId);
				if (chapter != null)
				{
					if(chapter.Directory != null && chapter.Directory != "")
					{
                       _fileSaver.FileDelete(chapter.Directory);
                    }
					
					if (_workService.DeleteChapterById(chapterId) > 0)
					{
						return Ok("Successful");
					}
					return BadRequest("Delete fail");
				}
				else return BadRequest("Chapter not found");
            
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}

