using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class BasketRepository : BaseRepository, IBasketRepository
    {
        public BasketRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<Basket> GetByIdAsync(int basketId)
        {
            return await _dbContext.Baskets.Include(b => b.Items).SingleOrDefaultAsync(b => b.Id == basketId);
        }

        public async Task AddAsync(Basket basket)
        {
            _dbContext.Baskets.Add(basket);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Basket basket)
        {
            _dbContext.Entry(basket).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
