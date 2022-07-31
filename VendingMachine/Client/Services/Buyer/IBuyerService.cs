using VendingMachine.Shared.Dto;

namespace VendingMachine.Client.Services.Buyer
{
    public interface IBuyerService
    {




        Task<DepositDto?> DepositAvailable();
        Task<int> TotalAmountOfDeposit();
        Task<DepositResultDto?> AddCoinToDeposit(CoinDto coinDto);
        Task<DepositResultDto?> ResetAllDeposit();

        Task<BuyerResultDto?> BuyProduc(ProductToPurchaseDto product);
        Task<List<CustomerProductDto>?> GetProductOrderedHistory();
       
    }
}
