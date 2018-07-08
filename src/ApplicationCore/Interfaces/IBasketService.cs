using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IBasketService
    {
        Task AddItemToBasket(int basketId, int productId, int quantity, decimal unitPrice);
        Task RemoveItemFromBasket(int basketId, int basketItemId);
        Task ClearAllItems(int basketId);
        Task ChangeItemQuantity(int basketId, int basketItemId, int quantity);
    }
}
