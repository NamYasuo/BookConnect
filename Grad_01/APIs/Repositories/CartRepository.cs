using System;
using APIs.DTO.Ecom;
using APIs.Repositories.Interfaces;
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

        public List<CartDetailsDTO> GetCartDetails(Guid userId)
            => new CartDAO().GetCartDetails(userId);
        public string GetUserCartId(Guid userId) => new CartDAO().GetUserCartId(userId);

        public void DeleteProductFromCart(Guid productId, Guid cartId, int quantity)
      => new CartDAO().DeleteProductFromCart(productId, cartId, quantity);

        public bool IsCartExist(Guid userId) => new CartDAO().IsCartExists(userId);
    }
}

