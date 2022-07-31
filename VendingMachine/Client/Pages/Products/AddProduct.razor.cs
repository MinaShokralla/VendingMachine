using AutoMapper;
using Microsoft.AspNetCore.Components;
using VendingMachine.Client.Services.Products;
using VendingMachine.Shared.Dto;

namespace VendingMachine.Client.Pages.Products
{
    public partial class AddProduct
    {
        [Inject] public IProductService _productService { get; set; }
        [Inject] public IMapper _mapper { get; set; }
        [Inject] public NavigationManager _navigationManager { get; set; }



        public ProductToCreateDto Product { get; set; } = new();
        public string message { get; set; } = string.Empty;




        async Task HandleCreateProduct()
        {
            try
            {
                var createdProduct = await _productService.CreateProduct(Product);
                if (createdProduct == null)
                {
                    message = "ERROR!!.. Product in not valid to Create";
                    return;
                }
                _navigationManager.NavigateTo($"/ProductDetail/{createdProduct.Id}");
            }
            catch (Exception)
            {
                message = "ERROR!!.. something went wrong, Please contact the admin..";
            }
        }
    }
}
