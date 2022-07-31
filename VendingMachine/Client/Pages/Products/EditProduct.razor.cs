using AutoMapper;
using Microsoft.AspNetCore.Components;
using VendingMachine.Client.Services.Products;
using VendingMachine.Shared.Dto;

namespace VendingMachine.Client.Pages.Products
{
    public partial class EditProduct
    {
        [Inject] public IProductService _productService { get; set; }
        [Inject] public IMapper _mapper { get; set; }


        [Parameter] public int ProductId { get; set; }

        public ProductToUpdateDto? Product { get; set; }
        public string message { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                message = string.Empty;
                var _product = await _productService.GetProductAsync(ProductId);
                if (_product == null)
                {
                    message = "ERROR!!.. The Product is not more validate";
                    return;
                }
                Product = _mapper.Map<ProductToUpdateDto>(_product);
            }
            catch (Exception)
            {
                message = "ERROR!!.. something went wrong, Please contact the admin..";
            }
        }


        async Task HandleUpdateProduct()
        {
            try
            {
                if (Product == null)
                {
                    message = "ERROR!!.. Product in not valid to modify";
                    return;
                }
                await _productService.UpdateProduct(ProductId, Product);
                message = "Success..Procut Update Successfully";
            }
            catch (Exception)
            {
                message = "ERROR!!.. something went wrong, Please contact the admin..";
            }
        }
    }
}
