using System;
using System.Collections.Generic;
using APIs.DTO.Ecom;
using BusinessObjects;
using BusinessObjects.Models;
using BusinessObjects.Models.Ecom;
using DataAccess.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class CartDAO
    {
        //Add Product to Cart
        //public int AddProductToCart(Guid productId, Guid cartId, int quantity)
        //{
        //	try
        //	{
        //		using(var context = new AppDbContext())
        //		{
        //			//insert into ListProducts(product_id, cart_id, order_id, quantity, added_date, stored_price)
        //			int result = context.Database.ExecuteSqlRaw
        //				($"exec AddProductToCart '{productId}', '{cartId}', null , {quantity} ,'{DateTime.Now}' , null");
        //			return result;
        //			//if (result == 1) return 1;
        //			//else return 0;
        //		}
        //	}
        //	catch(Exception e)
        //	{
        //		throw new Exception(e.Message);
        //	}
        //}

        public void AddListProductToCart(List<Guid> productIds, Guid cartId, int quantity)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    foreach (Guid id in productIds)
                    {
                        int result = context.Database.ExecuteSqlRaw
                        ($"exec AddProductToCart '{id}', '{cartId}', null , {quantity}, {DateTime.Now} , null");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<CartDetailsDTO> GetCartDetails(Guid userId)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var queryResult = from c in context.Carts
                                      join lp in context.Baskets on c.CartId equals lp.CartId
                                      join b in context.Books on lp.ProductId equals b.ProductId
                                      where c.CustomerId == userId
                                      select new
                                      {
                                          b.ProductId,
                                          b.Price,
                                          Stock = b.Quantity,
                                          b.Name,
                                          Quantity = lp.Quantity,
                                          c.CartId
                                      };

                    var resultList = queryResult.Select(r => new CartDetailsDTO
                    {
                        ProductId = r.ProductId,
                        Price = r.Price,
                        Stock = r.Stock,
                        Name = r.Name,
                        Quantity = r.Quantity,
                        CartId = r.CartId
                    }
                    ).ToList();
                    return resultList;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string GetUserCartId(Guid userId)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    Cart? userCart = context.Carts.Where(c => c.CustomerId == userId).FirstOrDefault();
                    if (userCart == null)
                    {
                        Cart newCart = new Cart()
                        {
                            CartId = Guid.NewGuid(),
                            CustomerId = userId,
                            Status = null,
                            CreateDate = DateTime.Now,
                            LastUpdatedDate = DateTime.Now,
                            Total_Quantity = null,
                            Total_Price = null,
                            ExpiredDate = DateTime.Now.AddDays(1)
                        };
                        context.Carts.Add(newCart);
                        int result = context.SaveChanges();
                        if (result > 0) return newCart.CartId.ToString();
                        else return "Fail to create cart";
                    }
                    else return userCart.CartId.ToString();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int AddProductToCart(Guid productId, Guid cartId, int quantity)
        {
            try
            {
                int result;
                using (var context = new AppDbContext())
                {

                    if (context.Baskets.Any(b => b.ProductId == productId && b.CartId == cartId))
                    {
                        result = context.Database.ExecuteSqlRaw(
                        "UPDATE Baskets " +
                        "SET Quantity = {0}, AddedDate = {1} " +
                        "WHERE ProductId = {2} AND CartId = {3}",
                        quantity,
                        DateTime.Now,
                        productId,
                        cartId);
                    }
                    else
                    {
                        string insertQuery = $"INSERT INTO Baskets (ProductId, CartId, OrderId, Quantity, AddedDate, Stored_Price) " +
                     $"VALUES (@productId, @cartId, NULL, @quantity, @addedDate, NULL)";

                        result = context.Database.ExecuteSqlRaw(insertQuery,
                            new SqlParameter("@productId", productId),
                            new SqlParameter("@cartId", cartId),
                            new SqlParameter("@quantity", quantity),
                            new SqlParameter("@addedDate", DateTime.Now));
                    }
                    return result;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int DeleteProductFromCart(Guid productId, Guid cartId, int quantity)
        {
            try
            {
                int result;
                using (var context = new AppDbContext())
                {
                    var basket = context.Baskets.FirstOrDefault(b => b.ProductId == productId && b.CartId == cartId);

                    if (basket != null)
                    {
                        if (basket.Quantity > quantity)
                        {
                            // If the quantity in the cart is greater than the quantity to delete, decrement it
                            result = context.Database.ExecuteSqlRaw(
                                "UPDATE Baskets SET Quantity = Quantity - {0} WHERE ProductId = {1} AND CartId = {2}",
                                quantity, productId, cartId);
                        }
                        else
                        {
                            // If the quantity to delete is greater than or equal to the quantity in the cart, delete the product
                            result = context.Database.ExecuteSqlRaw(
                                "DELETE FROM Baskets WHERE ProductId = {0} AND CartId = {1}",
                                productId, cartId);
                        }
                        return result;
                    }
                    else
                    {
                        // If the product is not found in the cart, return 0
                        return 0;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool IsCartExists(Guid userId)
        {
            try
            {
                using(var context = new AppDbContext())
                {
                    return context.Carts.Any(c => c.CustomerId == userId);
                }

            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        } 
    }
}

