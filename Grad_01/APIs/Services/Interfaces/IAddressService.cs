using APIs.Utils.Paging;
using BusinessObjects.Models;

namespace APIs.Services.Interfaces
{
	public interface IAddressService
	{
		int AddNewAddress(Address address);
		PagedList<Address> GetAllUserAddress(Guid userId, PagingParams @params);
		int UpdateAddressDefault(Address address);

    }
}

