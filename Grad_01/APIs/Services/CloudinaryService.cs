using System;
using CloudinaryDotNet;
using APIs.Services.Interfaces;
using BusinessObjects.DTO;
using CloudinaryDotNet.Actions;
using System.Net;
using System.Text.RegularExpressions;

namespace APIs.Services
{
	public class CloudinaryService: ICloudinaryService
	{

        private readonly string _imagePath = @"Data\Images\";
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

        public CloudinaryResponseDTO UploadImage(IFormFile uFile, string dir)
        {
            var client = new Cloudinary(account);
            var imageuploadParams = new ImageUploadParams()
            {
                Folder = dir,
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

        public CloudinaryResponseDTO DeleteImage(string imgUrl, string dir)
        {
            var client = new Cloudinary(account);
            string publicId = Regex.Match(imgUrl, $@"{account.Cloud}/image/upload/v\d+/(.*)\.\w+").Groups[1].Value;

            DeletionParams deletionParams = new DeletionParams(publicId);

            var result = client.Destroy(deletionParams);
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return new CloudinaryResponseDTO()
                {
                    StatusCode = (int)result.StatusCode,
                    Message = result.Error.Message
                };
            }
            if (result == null)
            {
                return new CloudinaryResponseDTO()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = "Undefied error!"
                };
            }
            return new CloudinaryResponseDTO()
            {
                StatusCode = (int)result.StatusCode,
                Message = "Delete successful!",
            };
        }

    }
}

