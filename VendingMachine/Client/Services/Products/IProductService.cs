using VendingMachine.Shared.Dto;

namespace VendingMachine.Client.Services.Products
{
    public interface IProductService
    {
        Task<List<ProductDto>?> GetAllProductsAsync();
        Task<List<ProductDto>?> GetMyProductsAsync();
        Task<ProductDto?> GetProductAsync(int ProductId);

        Task<ProductDto?> CreateProduct(ProductToCreateDto CreateProduct);
        Task<ProductDto?> UpdateProduct(int Id, ProductToUpdateDto UpdateProduct);
        Task DeleteProduct(int ProductId);
    }
}
