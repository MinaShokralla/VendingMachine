using VendingMachine.Models;
using VendingMachine.Shared.Dto;

namespace VendingMachine.WebAPI.Services.ProductService
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task<IEnumerable<ProductDto>> GetProductsForOneUserAsync(string username);
        Task<ProductDto?> GetProductAsync(int productId);
        Task<bool> DeleteProductAsync(int productId);

        Task<ProductDto> UpdateProductAsync(ProductToUpdateDto updateProduct, int ProductId);
        Task<ProductDto> CreateNewProductAsync(ProductToCreateDto createProduct, User user);

    }
}
