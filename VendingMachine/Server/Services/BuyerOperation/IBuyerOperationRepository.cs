using VendingMachine.Models;
using VendingMachine.Shared;
using VendingMachine.Shared.Dto;

namespace VendingMachine.WebAPI.Services.BuyerOperation
{
    public interface IBuyerOperationRepository
    {

        Task<DepositResultDto> DepositeAmout(CoinDto coinDto, int userId);
        Task<DepositResultDto> DepositeReset(int userId);

        Task<DepositDto?> DepoistAvailable(int userId);
        Task<int> DepoistTotalAmountAvailable(int userId);

        Task<ServiceResponse<bool>> ValidateTheAmountOfDepoist(ProductToPurchaseDto productToBuy, int userId);
        Task<BuyerResultDto> BuyProducts(ProductToPurchaseDto productToBuy, int userId);

        Task<IEnumerable<CustomerProductDto>?> GetCustomerProductHistory(int userId);
    }
}
