using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class BasketItemRepository : BaseRepository, IBasketItemRepository
    {
        public BasketItemRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task Add(BasketItem item)
        {
            throw new NotImplementedException();
        }
    }
}
