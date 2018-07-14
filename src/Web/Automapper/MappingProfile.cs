using ApplicationCore.Entities;
using AutoMapper;
using Web.Models;

namespace Web.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Basket, BasketViewModel>();
            CreateMap<BasketItem, BasketItemViewModel>();
            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductType, ProductTypeViewModel>();
        }
    }
}
