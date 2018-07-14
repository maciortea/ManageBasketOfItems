using ApplicationCore.Entities;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IBasketRepository
    {
        Task<Basket> GetByIdAsync(int id);
        Task<Basket> GetByUserId(string userId);
        Task AddAsync(Basket basket);
        Task UpdateAsync(Basket basket);
    }
}
