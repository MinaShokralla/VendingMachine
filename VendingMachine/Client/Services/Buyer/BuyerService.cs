using System.Net.Http.Json;
using VendingMachine.Shared.Dto;

namespace VendingMachine.Client.Services.Buyer
{
    public class BuyerService : IBuyerService
    {
        private readonly HttpClient _http;

        public BuyerService(HttpClient http)
        {
            _http = http;
        }



        public async Task<DepositDto?> DepositAvailable()
        {
            var result = await _http.GetFromJsonAsync<DepositDto?>("api/Buyer");
            return result;
        }
        public async Task<int> TotalAmountOfDeposit()
        {
            var result = await _http.GetFromJsonAsync<int>("api/Buyer/TotalDeposit");
            return result;
        }
        public async Task<List<CustomerProductDto>?> GetProductOrderedHistory()
        {
            var result = await _http.GetFromJsonAsync<List<CustomerProductDto>>("api/Buyer/GetOrderHistory");
            return result;
        }

        public async Task<DepositResultDto?> AddCoinToDeposit(CoinDto coinDto)
        {
            var result = await _http.PostAsJsonAsync("api/Buyer/Deposit", coinDto);
            var depositUpdated = (await result.Content.ReadFromJsonAsync<DepositResultDto>());
            return depositUpdated;
        }
        public async Task<DepositResultDto?> ResetAllDeposit()
        {
            var result = await _http.PutAsync("api/Buyer/Reset", null);
            var depositUpdated = (await result.Content.ReadFromJsonAsync<DepositResultDto>());
            return depositUpdated;
        }

        public async Task<BuyerResultDto?> BuyProduc(ProductToPurchaseDto product)
        {
            var result = await _http.PostAsJsonAsync("api/Buyer/Buy", product);
            var depositUpdated = await result.Content.ReadFromJsonAsync<BuyerResultDto>();
            return depositUpdated;
        }



    }
}
