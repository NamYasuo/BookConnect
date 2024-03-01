using System;
using APIs.DTO.Ecom;
using APIs.Repositories.Interfaces;
using BusinessObjects;
using DataAccess.DAO;

namespace APIs.Repositories
{
    public class CartRepository : ICartRepository
    {
        public void AddListProductToCart(List<Guid> productIds, Guid cartId, int quantity)
        => new CartDAO().AddListProductToCart(productIds, cartId, quantity);

        //AddProductToCart(Guid productId, Guid cartId, int quantity)
        public int AddProductToCart(Guid productId, Guid cartId, int quantity)
            => new CartDAO().AddProductToCart(productId, cartId, quantity);

        public void DeleteProductFromCart(Guid productId, Guid cartId)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    // Call the CartDAO method for deletion
                    new CartDAO().DeleteProductFromCart(productId, cartId);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<CartDetailsDTO> GetCartDetails(Guid userId)
            => new CartDAO().GetCartDetails(userId);
        public string GetUserCartId(Guid userId) => new CartDAO().GetUserCartId(userId);

        
    }
}

