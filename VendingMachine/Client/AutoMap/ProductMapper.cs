using AutoMapper;
using VendingMachine.Shared.Dto;

namespace VendingMachine.Client.AutoMap
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<ProductDto,ProductToUpdateDto>();
            CreateMap<ProductPurchasedDto,ProductDto > ();
        }
    }
}
