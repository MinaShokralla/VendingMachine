using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using VendingMachine.Client.Services.Buyer;
using VendingMachine.Client.Services.Products;
using VendingMachine.Shared.Dto;

namespace VendingMachine.Client.Pages.Buyer
{
    public partial class ProductToBuy
    {
        [Inject] public IProductService _productService { get; set; }
        [Inject] public IBuyerService _buyerService { get; set; }
        [Inject] public IMapper _mapper { get; set; }
        [Inject] public NavigationManager _navigationManager { get; set; }
        [Inject] public IJSRuntime JsRuntime { get; set; }

        [Parameter] public int ProductId { get; set; }


        public bool IsBusy { get; set; }
        public string message { get; set; } = string.Empty;
        public ProductDto? Product { get; set; }
        public ProductToPurchaseDto ProductModel { get; set; } = new();
        public int TotalDepositAmount { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Product = await _productService.GetProductAsync(ProductId);
                TotalDepositAmount = await _buyerService.TotalAmountOfDeposit();
                if (Product == null)
                {
                    message = "ERROR!!.. Product is not exist";
                }
                else
                {
                    ProductModel.productId = Product.Id;
                }
            }
            catch (Exception)
            {
                message = "ERROR!!.. something went wrong, Please contact the admin..";
            }
        }


        async Task HandleBuyProduct()
        {
            IsBusy = true;
            bool confirmed = await JsRuntime.InvokeAsync<bool>("confirm", $"You will buy {ProductModel.amountOfProduct} of Product ('{Product?.ProductName}')?");
            if (!confirmed)
            {
                IsBusy = false;
                return;
            }
            try
            {
                var OutResult = await _buyerService.BuyProduc(ProductModel);
                if (OutResult != null && OutResult.Succes)
                {
                    Product = _mapper.Map<ProductDto>(OutResult?.productPurchased);
                    message = $"SUCCESS!!..\n" +
                              $"you bought {ProductModel.amountOfProduct} of Product '{OutResult.productPurchased.ProductName}' \n" +
                              $"Total Amount of Cost you spend is {OutResult.AmountSpend} (cent)\n" +
                              $"Your available Cois {string.Join(" - ", OutResult.User.Deposit.Coins.OrderBy(c => c.CoinType).Select(c => $"({c.CoinType.ToString()}: {c.Count})").ToList())}";
                    TotalDepositAmount = await _buyerService.TotalAmountOfDeposit();
                }
                else if (OutResult != null)
                {
                    message = $"ERROR!!..\n {OutResult.Message}";
                }
                await Task.Delay(200);
            }
            catch (Exception)
            {
                IsBusy = false;
                message = "ERROR!!.. something went wrong, Please contact the admin..";
            }
            IsBusy = false;
        }

    }
}
