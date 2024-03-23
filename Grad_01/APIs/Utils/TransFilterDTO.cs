using System;
using APIs.Utils.Paging;
using BusinessObjects.Models.Ecom.Payment;
using Microsoft.AspNet.OData.Query;

namespace APIs.Utils
{
	public class TransFilterDTO
	{
       public ODataQueryOptions<TransactionRecord>? Odata { get; set; }
        public PagingParams @params { get; set; } = null!;
    }
}

