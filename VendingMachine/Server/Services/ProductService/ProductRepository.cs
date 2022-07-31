using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VendingMachine.Database;
using VendingMachine.Models;
using VendingMachine.Shared.Dto;

namespace VendingMachine.WebAPI.Services.ProductService
{
    public class ProductRepository : IProductRepository
    {
        private readonly ILogger<ProductRepository> _logger;
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;

        public ProductRepository(ILogger<ProductRepository> logger,
                                 DataContext dbContext,
                                 IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var LstProduct = await _dbContext.Products.Include(p => p.Seller).ToListAsync();
            var X = LstProduct;
            return _mapper.Map<List<ProductDto>>(LstProduct);
        }
        public async Task<IEnumerable<ProductDto>> GetProductsForOneUserAsync(string username)
        {
            var LstProduct = await _dbContext.Products
                                             .Include(p => p.Seller)
                                             .Where(p => p.Seller.Username == username)
                                             .ToListAsync();
            return _mapper.Map<List<ProductDto>>(LstProduct);
        }
        public async Task<ProductDto?> GetProductAsync(int productId)
        {
            var Product = await _dbContext.Products.Include(p => p.Seller).FirstOrDefaultAsync(p => p.Id == productId);
            if (Product == null) return null;
            return _mapper.Map<ProductDto>(Product);
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            var Product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (Product == null) return false;
            _dbContext.Remove(Product);
            var Out = await _dbContext.SaveChangesAsync();
            return Out > 0;
        }

        public async Task<ProductDto> UpdateProductAsync(ProductToUpdateDto updateProduct, int ProductId)
        {
            var Product = await _dbContext.Products.FindAsync(ProductId);
            _mapper.Map(updateProduct, Product!);
            var Out = await _dbContext.SaveChangesAsync();
            return _mapper.Map<ProductDto>(Product);
        }

        public async Task<ProductDto> CreateNewProductAsync(ProductToCreateDto createProduct, User user)
        {
            var Product = _mapper.Map<Product>(createProduct);
            Product.SellerId = user.Id;
            Product.Seller = user;
            _dbContext.Products.Add(Product);
            var Out = await _dbContext.SaveChangesAsync();
            return _mapper.Map<ProductDto>(Product);
        }




    }
}
