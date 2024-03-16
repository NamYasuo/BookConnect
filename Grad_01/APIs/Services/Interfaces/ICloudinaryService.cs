using System;
using BusinessObjects.DTO;

namespace APIs.Services.Interfaces
{
	public interface ICloudinaryService
	{
        string GetImagePath(IFormFile uFile);
        CloudinaryResponseDTO UploadImage(IFormFile uFile);
    }
}

