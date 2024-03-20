using System;
using Azure.Core;
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
                                item.Stored_Price = book.Price;
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

        public string TakeProductFromCartOptional(Guid userId, Guid orderId, List<ProductOptionDTO> productIds)
        {
            string hihi = "";
            try
            {
                using (var context = new AppDbContext())
                {
                    Cart? cart = context.Carts.Where(c => c.CustomerId == userId).FirstOrDefault();

                    if (cart != null)
                    {
                        //List<Basket> basketItems = new List<Basket>();
                        foreach (ProductOptionDTO dto in productIds)
                        {
                            Basket? temp = context.Baskets.Where(b => b.ProductId == dto.ProductId && b.CartId == cart.CartId).FirstOrDefault();
                            if (temp != null)
                            {
                                temp.CartId = null;
                                temp.OrderId = orderId;
                                Book? book = context.Books.Where(b => b.ProductId == temp.ProductId).FirstOrDefault();

                                if (book != null)
                                {
                                    temp.Stored_Price = book.Price;
                                }
                                else { return "Error! Product doesn't exist, productId: " + temp.ProductId; }

                                int clear = temp.Quantity - dto.Quantity;

                                hihi = "IN CART:" + temp.Quantity.ToString() +" | CHECKOUT: " + dto.Quantity.ToString();

                                if (clear <= 0)
                                {
                                    context.Database.ExecuteSqlRaw("UPDATE Baskets SET CartId = NULL, OrderId = {0}, Stored_Price = {1} WHERE ProductId = {2} AND CartId is not null AND OrderId is null", orderId, temp.Stored_Price, temp.ProductId);
                                }
                                else
                                {
                                    context.Database.ExecuteSqlRaw("UPDATE Baskets SET Quantity = Quantity - {0} WHERE ProductId = {1} AND CartId is not null AND OrderId is null", dto.Quantity, dto.ProductId);
                                    context.Database.ExecuteSqlRaw("insert into Baskets(ProductId, OrderId, Quantity, AddedDate, Stored_Price) values ({0}, {1}, {2}, {3}, {4})", dto.ProductId, orderId, dto.Quantity, DateTime.Now, temp.Stored_Price);
                                }
                            }
                        }
                            //int result = context.Baskets.Where(o => o.OrderId == orderId).Count();
                            //if (result == 1)
                            //{
                                //return "Successfully!";
                                return hihi;
                            //}
                            //else return "Add Fail!";
                    }
                     return "Cart doesn't existed!!!";
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
                        Total_Price = data.Price,
                        Status = data.Status,
                        Quantity = 0,
                        Notes = data.Notes,
                        CreatedDate = DateTime.Now,
                        PaymentMethod = data.PaymentMethod,
                        TransactionId = data.TransactionId,
                        AddressId = data.AddressId
                };
                    context.Orders.Add(newOrder);
                    int result = context.SaveChanges();
                    if (result == 1)
                    {
                        return "Successfully!";
                    }
                    else return "Fail to create new order!!!";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public decimal GetTotalAmount(List<ProductOptionDTO> dto)
        {
            try
            {
                decimal result = 0;
                using(var context = new AppDbContext())
                {
                    foreach(ProductOptionDTO c in dto)
                    {
                       Book? temp = context.Books.Where(b => b.ProductId == c.ProductId).FirstOrDefault();
                        if(temp != null)
                        {
                            result += temp.Price*c.Quantity;
                        }
                    }
                }
                return result;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}

