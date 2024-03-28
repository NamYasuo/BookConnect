﻿using System;
using System.Collections.Generic;
using APIs.Services.Interfaces;
using BusinessObjects.DTO;
using BusinessObjects.Models.Ecom;
using DataAccess.DAO;
using DataAccess.DAO.Ecom;

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

        public int GetCurrentStock(Guid productId) => new AgencyDAO().GetProductStock(productId);


        public List<Order> GetUserOrders(Guid userId) => new OrderDAO().GetUserOrders(userId);
        

    }
}

