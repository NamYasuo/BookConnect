using System;
using System.Collections.Generic;
using APIs.Services.Interfaces;
using BusinessObjects.DTO;
using DataAccess.DAO;

namespace APIs.Services
{
	public class OrderService: IOrderService
	{
        public string TakeProductFromCart(Guid userId, Guid orderId) => new OrderDAO().TakeProductFromCart(userId, orderId);

        public string CreateNewOrder(NewOrderDTO data) => new OrderDAO().CreateNewOrder(data);

        public string TakeProductFromCartOptional (Guid userId, Guid orderId, List<ProductOptionDTO> products)
        => new OrderDAO().TakeProductFromCartOptional(userId, orderId, products);

        public decimal GetTotalAmount(List<ProductOptionDTO> productIds)
        => new OrderDAO().GetTotalAmount(productIds);
    }
}

