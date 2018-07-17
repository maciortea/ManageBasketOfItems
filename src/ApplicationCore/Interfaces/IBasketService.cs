using ApplicationCore.Entities;
using CSharpFunctionalExtensions;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IBasketService
    {
        Task AddBasketAsync(Basket basket);
        Task<Basket> GetBasketByUserIdAsync(string userId);
        Task<Result<int>> AddItemToBasketAsync(int basketId, int productId, int quantity, Pounds unitPriceInPounds);
        Task<Result> RemoveItemFromBasketAsync(string userId, int basketItemId);
        Task<Result> ClearAllItemsAsync(string userId);
        Task<Result> ChangeItemQuantityAsync(string userId, int basketItemId, int quantity);
    }
}
