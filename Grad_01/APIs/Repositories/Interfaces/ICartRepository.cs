using System;
using APIs.DTO.Ecom;

namespace APIs.Repositories.Interfaces
{
	public interface ICartRepository
	{
		public int AddProductToCart(Guid productId, Guid cartId, int quantity);
		public List<CartDetailsDTO> GetCartDetails(Guid userId);
		public void AddListProductToCart(List<Guid> productIds, Guid cartId, int quantity);
		public string GetUserCartId(Guid userId);
        public void DeleteProductFromCart(Guid productId, Guid cartId, int quantity);
		public bool IsCartExist(Guid userId);
    }
}

