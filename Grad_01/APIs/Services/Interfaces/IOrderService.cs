using System;
using BusinessObjects.DTO;
using DataAccess.DAO;

namespace APIs.Services.Interfaces
{
	public interface IOrderService
	{
        public string TakeProductFromCart(Guid userId, Guid orderId);

        public string CreateNewOrder(NewOrderDTO data);

        public string TakeProductFromCartOptional (Guid userId, Guid orderId, List<ProductOptionDTO> products);

        public decimal GetTotalAmount(List<ProductOptionDTO> dto);

        public int GetCurrentStock(Guid productId);
    }
}

