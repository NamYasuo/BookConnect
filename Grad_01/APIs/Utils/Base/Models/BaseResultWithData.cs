using System;
namespace APIs.Utils.Base.Models
{
	public class BaseResultWithData<T> : BaseResult
	{
        public T? Data { get; set; }

        public void Set(bool success, string message, T data)
        {
            this.Data = data;
            this.Success = success;
            this.Message = message;
        }
    }
}

