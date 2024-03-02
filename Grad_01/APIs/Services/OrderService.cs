using System;
using APIs.Services.Interfaces;
using BusinessObjects.DTO;
using DataAccess.DAO;

namespace APIs.Services
{
	public class OrderService: IOrderService
	{
        public string TakeProductFromCart(Guid userId, Guid orderId) => new OrderDAO().TakeProductFromCart(userId, orderId);

        public string CreateNewOrder(NewOrderDTO data) => new OrderDAO().CreateNewOrder(data);

    }
}

