using System.Threading.Tasks;
using Web.Models;

namespace Web.Services
{
    public interface IBasketViewModelService
    {
        Task<BasketViewModel> GetOrCreateBasketForUserAsync(string userName);
        Task<BasketItemViewModel> GetBasketItemForUserAsync(string userName, int basketItemId);
    }
}
