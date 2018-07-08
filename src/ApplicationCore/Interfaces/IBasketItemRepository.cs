using ApplicationCore.Entities;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IBasketItemRepository
    {
        Task Add(BasketItem item);
    }
}
