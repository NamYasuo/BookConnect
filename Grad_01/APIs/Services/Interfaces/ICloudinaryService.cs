using System;
using BusinessObjects.DTO;

namespace APIs.Services.Interfaces
{
	public interface ICloudinaryService
	{
        CloudinaryResponseDTO UploadImage(IFormFile uFile, string dir);
        CloudinaryResponseDTO DeleteImage(string imgUrl);
    }
}

