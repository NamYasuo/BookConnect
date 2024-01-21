using System;
using BusinessObjects;
using BusinessObjects.DTO;
using BusinessObjects.Models;
using BusinessObjects.Models.Ecom;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class OrderDAO
    {
        //Cart to Order
        public string TakeProductFromCart(Guid userId, Guid orderId)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    Cart? cart = context.Carts.Where(c => c.CustomerId == userId).FirstOrDefault();

                    if (cart != null)
                    {
                        var basketItems = context.Baskets.Where(b => b.CartId == cart.CartId).ToList();

                        foreach (var item in basketItems)
                        {
                            item.CartId = null;
                            item.OrderId = orderId;
                            Book? book = context.Books.Where(b => b.ProductId == item.ProductId).FirstOrDefault();

                            if (book != null)
                            {
                                item.Stored_Price = (decimal?)book.Price;
                            }
                            else return "Error! Product doesn't exist, productId: " + item.ProductId;
                            context.Database.ExecuteSqlRaw("UPDATE Baskets SET CartId = NULL, OrderId = {0}, Stored_Price = {1} WHERE ProductId = {2} AND CartId is not null AND OrderId is null", orderId, item.Stored_Price, item.ProductId);
                        }
                        int result = context.Baskets.Where(o => o.OrderId == orderId).Count();
                        if (result == 1)
                        {
                            return "Successfully!";
                        }
                        else return "Add Fail!";
                    }
                    else return "Cart doesn't existed!!!";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string CreateNewOrder(NewOrderDTO data)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    decimal? total_Price = 0;

                    Order newOrder = new Order
                    {
                        OrderId = data.OrderId,
                        CustomerId = data.CustomerId,
                        Total_Price = total_Price,
                        Status = data.Status,
                        Quantity = 0,
                        Notes = data.Notes,
                        CreatedDate = DateTime.Now,
                        //PaymentId = data.PaymentId,
                        AddressId = data.AddressId
                    };
                    context.Orders.Add(newOrder);
					int result = context.SaveChanges();
                    if (result == 1)
                    {
                        string result0 = TakeProductFromCart(data.CustomerId, data.OrderId);
                        if (result0 == "Successfully!")
                        {
                            List<Basket> listItems = context.Baskets.Where(b => b.OrderId == data.OrderId).ToList();
                            foreach (Basket b in listItems)
                            {
                                total_Price += b.Stored_Price;
                            }
                            Order? order = context.Orders.Where(o => o.OrderId == data.OrderId).FirstOrDefault();
                            if (order != null)
                            {
                                order.Quantity = listItems.Count();
                                order.Total_Price = total_Price;
                            }
                            else return "Order not yet created!!!";
                            int result2 = context.SaveChanges();
                            if (result2 == 1) return "Successfully!";
                            else return "Update quantity and price fail!!!";
                        } else return result0;
                     }
                    else return "Fail to create new order!!!";
                }
            }
			catch(Exception e)
			{
    throw new Exception(e.Message);
}
        }
	}
}

