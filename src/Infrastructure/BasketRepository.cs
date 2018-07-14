using System.Linq;
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

        public async Task<Basket> GetByIdAsync(int id)
        {
            return await GetBasketWithAllIncludesQuery().SingleOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Basket> GetByUserIdAsync(string userId)
        {
            return await GetBasketWithAllIncludesQuery().SingleOrDefaultAsync(b => b.UserId == userId);
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

        private IQueryable<Basket> GetBasketWithAllIncludesQuery()
        {
            return _dbContext.Baskets
                .Include(b => b.Items)
                    .ThenInclude(i => i.Product)
                        .ThenInclude(p => p.ProductType);
        }
    }
}
