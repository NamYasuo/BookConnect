using System;
using BusinessObjects.DTO;

namespace APIs.Services.Interfaces
{
	public interface ICloudinaryService
	{
        CloudinaryResponseDTO UploadImage(IFormFile imgFile, string dir);
        CloudinaryResponseDTO DeleteImage(string imgUrl, string dir);
    }
}

