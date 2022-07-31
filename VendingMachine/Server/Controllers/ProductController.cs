using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VendingMachine.Shared.Dto;
using VendingMachine.WebAPI.Services.AuthService;
using VendingMachine.WebAPI.Services.ProductService;

namespace VendingMachine.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IAuthService _authService;
        private readonly IProductRepository _productRepository;

        public ProductController(ILogger<ProductController> logger,
                                 IAuthService authService,
                                 IProductRepository productRepository)
        {
            _logger = logger;
            _authService = authService;
            _productRepository = productRepository;
        }


        [AllowAnonymous, HttpGet("Products")]
        public async Task<ActionResult<List<ProductDto>>> GetProducts()
        {
            _logger.LogInformation($"Product api called to get all Products");
            var X = Request;
            var result = await _productRepository.GetProductsAsync();
            return Ok(result);
        }
        [HttpGet("MyProducts")]
        public async Task<ActionResult<List<ProductDto>>> GetMyProducts()
        {
            _logger.LogInformation($"Product api called to get My Products only");
            var result = await _productRepository.GetProductsForOneUserAsync(User.Identity!.Name!);
            return Ok(result);
        }
        [HttpGet("Products/{Id}")]
        public async Task<ActionResult<ProductDto>> GetOneProduct(int Id)
        {
            _logger.LogInformation($"Product api called to get One Product with id {Id}");
            var product = await _productRepository.GetProductAsync(Id);
            if (product == null) return NotFound();
            //if (product?.Seller.Username != User.Identity?.Name!) return Forbid();
            return Ok(product);
        }


        [HttpDelete("Products/{Id}"), Authorize(Roles = "Seller")]
        public async Task<ActionResult> DeleteMyProduct(int Id)
        {
            _logger.LogInformation($"Product api called to Delete ProductId {Id}");
            var product = await _productRepository.GetProductAsync(Id);
            if (product == null) return NotFound();
            if (product?.Seller.Username != User.Identity?.Name!) return Forbid();
            var result = await _productRepository.DeleteProductAsync(Id);
            if (!result) return BadRequest();
            return NoContent();
        }


        [HttpPut("{Id}"), Authorize(Roles = "Seller,Admin")]
        public async Task<ActionResult<ProductDto>> UpdateProduct(int Id, ProductToUpdateDto UpdateProduct)
        {
            _logger.LogInformation($"Product api called to update ProductId {Id}");
            var product = await _productRepository.GetProductAsync(Id);
            if (product == null) return NotFound();
            if (product.SellerId != _authService.GetUserId()) return Forbid();
            var result = await _productRepository.UpdateProductAsync(UpdateProduct, Id);
            if (result == null) return BadRequest();
            return Ok(result);
        }


        [HttpPost, Authorize(Roles = "Seller,Admin")]
        public async Task<ActionResult<ProductDto>> CreateProduct(ProductToCreateDto CreateProduct)
        {
            _logger.LogInformation($"Product api called to Create a new Product");
            var user = await _authService.GetUserByUsername(_authService.GetUsername());
            if (user == null) return BadRequest();
            var result = await _productRepository.CreateNewProductAsync(CreateProduct, user);
            if (result == null) return BadRequest();
            return Created($"Products/{result.Id}", result);
        }

    }
}
