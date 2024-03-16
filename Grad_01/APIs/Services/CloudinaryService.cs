using System;
using CloudinaryDotNet;
using APIs.Services.Interfaces;
using BusinessObjects.DTO;
using CloudinaryDotNet.Actions;
using System.Net;

namespace APIs.Services
{
	public class CloudinaryService: ICloudinaryService
	{

        private readonly string _imagePath = @"Data\Images\Venues";
        private readonly IConfiguration _config;
        private readonly Account account;

        public CloudinaryService(IConfiguration config)
		{
            _config = config;
            account = new Account(
                 config.GetSection("Cloudinary")["CloudName"],
                 config.GetSection("Cloudinary")["ApiKey"],
                 config.GetSection("Cloudinary")["ApiSerect"]
                );
        }

        public string GetImagePath(IFormFile uFile) => Path.Combine(_imagePath, uFile.FileName);


        public CloudinaryResponseDTO UploadImage(IFormFile uFile)
        {
            var client = new Cloudinary(account);
            var imageuploadParams = new ImageUploadParams()
            {
                File = new FileDescription(uFile.FileName, uFile.OpenReadStream()),
                DisplayName = uFile.FileName
            };
            var uploadResult = client.Upload(imageuploadParams);
            if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return new CloudinaryResponseDTO()
                {
                    StatusCode = (int)uploadResult.StatusCode,
                    Message = uploadResult.Error.Message
                };
            }
            if (uploadResult == null)
            {
                return new CloudinaryResponseDTO()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = "Undefied error!"
                };
            }
            return new CloudinaryResponseDTO()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Upload successful!",
                Data = uploadResult.SecureUrl.ToString()
            };
        }
    }
}

