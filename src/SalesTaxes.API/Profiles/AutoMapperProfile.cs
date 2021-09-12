using AutoMapper;
using SalesTaxes.API.ViewModel;
using SalesTaxes.Domain.ValueObjects;

namespace SalesTaxes.API.Profiles
{
    public class AutoMapperConfigProfile : Profile
    {
        public AutoMapperConfigProfile()
        {
            CreateMap<Sale, SaleResultViewModel>().ReverseMap();

            CreateMap<SaleItem, SaleItemViewModel>().ReverseMap();
            CreateMap<SaleItem, SaleItemResultViewModel>().ReverseMap();
        }
    }
}