﻿using System;
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
        //---------------------------------------------------------IMAGE---------------------------------------------------------//

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

        public bool IsImageExisted(string imgUrl, string dir)
        {
            var client = new Cloudinary(account);
            string publicId = Regex.Match(imgUrl, $@"{account.Cloud}/image/upload/v\d+/(.*)\.\w+").Groups[1].Value;
            GetResourceResult resourceResult = client.GetResource(publicId);

            return (resourceResult != null && resourceResult.StatusCode == HttpStatusCode.OK);
        }

        public CloudinaryResponseDTO DeleteImage(string imgUrl)
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
        //---------------------------------------------------------VIDEO---------------------------------------------------------//

        // Upload Video
        public CloudinaryResponseDTO UploadVideo(IFormFile uFile, string dir)
        {
            var client = new Cloudinary(account);
            var VideoUploadParams = new VideoUploadParams()
            {
                Folder = dir,
                File = new FileDescription(uFile.FileName, uFile.OpenReadStream()),
                DisplayName = uFile.FileName
            };
            var uploadResult = client.Upload(VideoUploadParams);
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

        //Check existed video
        public bool IsVideoExisted(string vidUrl, string dir)
        {
            var client = new Cloudinary(account);
            string publicId = Regex.Match(vidUrl, $@"{account.Cloud}/video/upload/v\d+/(.*)\.\w+").Groups[1].Value;
            GetResourceResult resourceResult = client.GetResource(publicId);

            return (resourceResult != null && resourceResult.StatusCode == HttpStatusCode.OK);
        }

        // Delete Video
        public CloudinaryResponseDTO DeleteVideo(string vidUrl)
        {
            var client = new Cloudinary(account);
            string publicId = Regex.Match(vidUrl, $@"{account.Cloud}/video/upload/v\d+/(.*)\.\w+").Groups[1].Value;

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

