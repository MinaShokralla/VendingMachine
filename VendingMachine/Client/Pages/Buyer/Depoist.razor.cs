using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using VendingMachine.Client.Services.Buyer;
using VendingMachine.Models;
using VendingMachine.Shared;
using VendingMachine.Shared.Dto;

namespace VendingMachine.Client.Pages.Buyer
{
    public partial class Depoist
    {

        [Inject] public IBuyerService _buyerService { get; set; }
        [Inject] public IJSRuntime JsRuntime { get; set; }

        public bool IsBusy { get; set; }
        public string message { get; set; } = string.Empty;

        public int TotalDepositAmount { get; set; }

        public int Coin5Amount { get; set; }
        public int Coin10Amount { get; set; }
        public int Coin20Amount { get; set; }
        public int Coin50Amount { get; set; }
        public int Coin100Amount { get; set; }


        protected override async Task OnInitializedAsync()
        {
            IsBusy = true;
            try
            {
                var deposit = await _buyerService.DepositAvailable();
                ColectCoinsAndUpdateUI(deposit);
                await Task.Delay(500);
            }
            catch (Exception)
            {                
            }
            IsBusy = false;
        }


        async Task HandleResetDepoistAll()
        {
            IsBusy = true;
            bool confirmed = await JsRuntime.InvokeAsync<bool>("confirm", $"Are you sure you want to Reset all your deposit?");
            if (!confirmed)
            {
                IsBusy = false;
                return;
            }
            try
            {
                var newdeposit = await _buyerService.ResetAllDeposit();
                ColectCoinsAndUpdateUI(newdeposit?.User.Deposit);
                await Task.Delay(500);
                message = $"SUCCESS!!.. You resetted all cents in your deposit ";
            }
            catch (Exception)
            {
                IsBusy = false;
                message = "ERROR!!.. something went wrong, Please contact the admin..";
            }
            IsBusy = false;
        }



        async Task HandleDepositCoin(CoinTypeEnum coinType)
        {
            IsBusy = true;
            bool confirmed = await JsRuntime.InvokeAsync<bool>("confirm", $"Are you sure you want to deposit coin '{(int)coinType}' cents)?");
            if (!confirmed)
            {
                IsBusy = false;
                return;
            }
            try
            {                
                var newdeposit = await _buyerService.AddCoinToDeposit(new CoinDto { CoinType = coinType, Count = 1 });
                ColectCoinsAndUpdateUI(newdeposit?.User.Deposit);
                await Task.Delay(500);
                message = $"SUCCESS!!.. You added '{(int)coinType}' cents in your deposit ";
            }
            catch (Exception)
            {
                IsBusy = false;
                message = "ERROR!!.. something went wrong, Please contact the admin..";
            }
            IsBusy = false;

        }


        private void ColectCoinsAndUpdateUI(DepositDto? deposit)
        {            
            if (deposit?.Coins == null) return;
            var coinsLst = deposit.Coins.ToList();
            TotalDepositAmount = coinsLst.Sum(c => c.Count * (int)c.CoinType);
            Coin5Amount = GetAvailableCoinsForEachType(coinsLst, CoinTypeEnum.Coin5Cent);
            Coin10Amount = GetAvailableCoinsForEachType(coinsLst, CoinTypeEnum.Coin10Cent);
            Coin20Amount = GetAvailableCoinsForEachType(coinsLst, CoinTypeEnum.Coin20Cent);
            Coin50Amount = GetAvailableCoinsForEachType(coinsLst, CoinTypeEnum.Coin50Cent);
            Coin100Amount = GetAvailableCoinsForEachType(coinsLst, CoinTypeEnum.Coin100Cent);
        }
        private int GetAvailableCoinsForEachType(List<CoinDto> coins, CoinTypeEnum CoinType)
        {
            var coin = coins.FirstOrDefault(c => c.CoinType == CoinType);
            if (coin == null) return 0;
            return coin.Count;
        }


    }
}
