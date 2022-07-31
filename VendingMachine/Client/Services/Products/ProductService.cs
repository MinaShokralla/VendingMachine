using System.Net.Http.Json;
using VendingMachine.Shared.Dto;

namespace VendingMachine.Client.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _http;

        public ProductService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ProductDto>?> GetAllProductsAsync()
        {
            var result = await _http.GetFromJsonAsync<List<ProductDto>>("api/product/Products");
            return result;
        }
        public async Task<List<ProductDto>?> GetMyProductsAsync()
        {
            var result = await _http.GetFromJsonAsync<List<ProductDto>>("api/product/MyProducts");
            return result;
        }
        public async Task<ProductDto?> GetProductAsync(int ProductId)
        {
            var result = await _http.GetFromJsonAsync<ProductDto>($"api/Product/Products/{ProductId}");
            return result;
        }
        public async Task<ProductDto?> CreateProduct(ProductToCreateDto CreateProduct)
        {
            var result = await _http.PostAsJsonAsync("api/product", CreateProduct);
            var newProduct = (await result.Content.ReadFromJsonAsync<ProductDto>());
            return newProduct;
        }
        public async Task DeleteProduct(int ProductId)
        {
            await _http.DeleteAsync("api/product/Products/" + ProductId);
        }
        public async Task<ProductDto?> UpdateProduct(int Id, ProductToUpdateDto UpdateProduct)
        {
            var result = await _http.PutAsJsonAsync($"api/product/{Id}", UpdateProduct);
            var newProduct = (await result.Content.ReadFromJsonAsync<ProductDto>());
            return newProduct;
        }
    }
}
