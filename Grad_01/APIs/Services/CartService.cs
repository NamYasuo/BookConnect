using System;
using APIs.DTO.Ecom;
using APIs.Services.Intefaces;
using BusinessObjects;
using DataAccess.DAO;

namespace APIs.Services
{
    public class CartService : ICartService
    {
        public void AddListProductToCart(List<Guid> productIds, Guid cartId, int quantity)
        => new CartDAO().AddListProductToCart(productIds, cartId, quantity);

        //AddProductToCart(Guid productId, Guid cartId, int quantity)
        public int AddProductToCart(Guid productId, Guid cartId, int quantity)
            => new CartDAO().AddProductToCart(productId, cartId, quantity);

        

        public void DeleteProductFromCart(Guid productId, Guid cartId, int quantity)
         =>new CartDAO().DeleteProductFromCart(productId, cartId, quantity);

        

        // Call the CartDAO method for deletion

        public List<CartDetailsDTO> GetCartDetails(Guid userId)
            => new CartDAO().GetCartDetails(userId);
        public string GetUserCartId(Guid userId) => new CartDAO().GetUserCartId(userId);


    }
}

