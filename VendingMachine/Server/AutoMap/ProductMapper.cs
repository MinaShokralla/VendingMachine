using AutoMapper;
using VendingMachine.Models;
using VendingMachine.Shared.Dto;

namespace VendingMachine.WebAPI.AutoMap
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<Product, ProductDto>()
                     .ForMember(p => p.Seller, opt => opt.MapFrom(s => s.Seller))
                     .ReverseMap();
            CreateMap<ProductToUpdateDto, Product>();
            CreateMap<ProductToCreateDto, Product>();

            CreateMap<Product, CustomerProduct>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(p => p.UserId, opt => opt.Ignore())
                .ForMember(p => p.ProductId, opt => opt.Ignore())
                .ForMember(p => p.BoughtOn, opt => opt.Ignore())
                .ForMember(p => p.RowVersion, opt => opt.Ignore());

            CreateMap<Product, ProductPurchasedDto>();
            CreateMap<CustomerProduct, CustomerProductDto>();

        }
    }
}
