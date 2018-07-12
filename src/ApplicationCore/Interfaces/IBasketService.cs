using ApplicationCore.Entities;
using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IBasketService
    {
        Task<Result<Basket>> GetBasketById(int basketId);
        Task<Basket> GetBasketByUserName(string userName);
        Task<Result<IReadOnlyCollection<BasketItem>>> GetBasketItems(int basketId);
        Task<Result> AddItemToBasket(int basketId, int productId, int quantity, decimal unitPrice);
        Task<Result> RemoveItemFromBasket(int basketId, int basketItemId);
        Task<Result> ClearAllItems(int basketId);
        Task<Result> ChangeItemQuantity(int basketId, int basketItemId, int quantity);
    }
}
