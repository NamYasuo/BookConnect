using System;
namespace APIs.Utils.Base.Models
{
	public class BasePagingQuery
	{
        public string? Criteria { get; set; } = string.Empty;
        public int? PageSize { get; set; } = 20;
        public int? PageIndex { get; set; } = 1;
    }
}

