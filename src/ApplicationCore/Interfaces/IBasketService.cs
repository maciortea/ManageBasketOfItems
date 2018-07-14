using ApplicationCore.Entities;
using CSharpFunctionalExtensions;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IBasketService
    {
        Task AddBasket(Basket basket);
        Task<Basket> GetBasketByUserId(string userId);
        Task<Result> AddItemToBasket(int basketId, int productId, int quantity, decimal unitPrice);
        Task<Result> RemoveItemFromBasket(string userId, int basketItemId);
        Task<Result> ClearAllItems(string userId);
        Task<Result> ChangeItemQuantity(string userId, int basketItemId, int quantity);
    }
}
