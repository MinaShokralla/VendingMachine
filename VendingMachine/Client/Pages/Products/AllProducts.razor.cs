using Microsoft.AspNetCore.Components;
using VendingMachine.Client.Services.Products;
using VendingMachine.Shared.Dto;

namespace VendingMachine.Client.Pages.Products
{
    public partial class AllProducts
    {

        [Inject] public IProductService _productService { get; set; }



        public List<ProductDto>? Products { get; set; }

        public string message { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            try
            {

                Products = await _productService.GetAllProductsAsync();
            }
            catch (Exception)
            {
                message = "ERROR!!.. something went wrong, Please contact the admin..";
            }
        }
    }
}
