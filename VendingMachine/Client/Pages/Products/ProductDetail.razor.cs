using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using VendingMachine.Client.Services.Products;
using VendingMachine.Shared.Dto;

namespace VendingMachine.Client.Pages.Products
{
    public partial class ProductDetail
    {
        [Inject] public IProductService _productService { get; set; }
        [Inject] public IMapper _mapper { get; set; }
        [Inject] public NavigationManager _navigationManager { get; set; }
        [Inject] public IJSRuntime JsRuntime { get; set; }

        [Parameter] public int ProductId { get; set; }


        public string message { get; set; } = string.Empty;
        public ProductDto? Product { get; set; }


        protected override async Task OnInitializedAsync()
        {
            try
            {
                Product = await _productService.GetProductAsync(ProductId);
                if (Product == null)
                {
                    message = "ERROR!!.. Product is not exist";
                }
            }
            catch (Exception)
            {
                message = "ERROR!!.. something went wrong, Please contact the admin..";
            }
        }


        async Task HandleDeleteProduct()
        {
            bool confirmed = await JsRuntime.InvokeAsync<bool>("confirm", $"Are you sure to delete the product '{Product?.ProductName}')?");
            if (!confirmed) return;
            try
            {
                await _productService.DeleteProduct(ProductId);
                _navigationManager.NavigateTo("/DeleteMessage");
            }
            catch (Exception)
            {
                message = "ERROR!!.. Cannot delete this product, Please contact the Admin";
            }
        }
    }
}
