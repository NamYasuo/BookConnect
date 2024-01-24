using System;
using Microsoft.AspNetCore.Http;

namespace DataAccess.DTO.Creative
{
	public class FileUploadDTO
	{
		public IFormFile? File { get; set; }
	}
}

