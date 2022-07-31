using AutoMapper;
using VendingMachine.Models;
using VendingMachine.Shared.Dto;

namespace VendingMachine.WebAPI.AutoMap
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserForProductDto>();
            CreateMap<UserForProductDto, User>()
                .ForMember(m => m.PasswordHash, opt => opt.Ignore())
                .ForMember(m => m.PasswordSalt, opt => opt.Ignore());
            CreateMap<User, UserBuyerDto>();
            CreateMap<Deposit, DepositDto>().ReverseMap();
            CreateMap<Deposit, DepositResultDto>();
            CreateMap<Coin, CoinDto>();
        }
    }
}
