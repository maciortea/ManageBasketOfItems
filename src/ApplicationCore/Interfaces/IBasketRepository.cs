using ApplicationCore.Entities;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IBasketRepository
    {
        Task<Basket> GetByIdAsync(int basketId);
        Task<Basket> GetByUserName(string userName);
        Task AddAsync(Basket basket);
        Task UpdateAsync(Basket basket);
    }
}
