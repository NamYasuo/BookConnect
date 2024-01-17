using System;
using APIs.DTO.Ecom;
using BusinessObjects;
using BusinessObjects.Models;
using BusinessObjects.Models.Ecom;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
	public class CartDAO
	{
		//Add Product to Cart
		public int AddProductToCart(Guid productId, Guid cartId, int quantity)
		{
			try
			{
				using(var context = new AppDbContext())
				{
					//insert into ListProducts(product_id, cart_id, order_id, quantity, added_date, stored_price)
					int result = context.Database.ExecuteSqlRaw
						($"exec AddProductToCart '{productId}', '{cartId}', null , {quantity} ,'{DateTime.Now}' , null");
					return result;
					//if (result == 1) return 1;
					//else return 0;
				}
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

        public void AddListProductToCart(List<Guid> productIds, Guid cartId , int quantity)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    foreach(Guid id in productIds)
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


    }
}

