using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VendingMachine.Shared.Dto;
using VendingMachine.WebAPI.Services.AuthService;
using VendingMachine.WebAPI.Services.ProductService;
using VendingMachine.WebAPI.Services.BuyerOperation;

namespace VendingMachine.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Buyer,Admin")]
    public class BuyerController : Controller
    {
        private readonly ILogger<BuyerController> _logger;
        private readonly IAuthService _authService;
        private readonly IProductRepository _productRepository;
        private readonly IBuyerOperationRepository _buyerOperationRepository;

        public BuyerController(ILogger<BuyerController> logger,
                                 IAuthService authService,
                                 IProductRepository productRepository,
                                 IBuyerOperationRepository buyerOperationRepository)
        {
            _logger = logger;
            _authService = authService;
            _productRepository = productRepository;
            _buyerOperationRepository = buyerOperationRepository;
        }



        [HttpGet]
        public async Task<ActionResult<DepositDto?>> GetDepoistAvailable()
        {
            _logger.LogInformation($"Buyer api called to get all Deposit");
            var result = await _buyerOperationRepository.DepoistAvailable(_authService.GetUserId());
            return Ok(result);
        }

        [HttpGet("GetOrderHistory")]
        public async Task<ActionResult<List<CustomerProductDto>>> GetOrderHistory()
        {
            _logger.LogInformation($"Buyer api called to get all order history");
            var result = await _buyerOperationRepository.GetCustomerProductHistory(_authService.GetUserId());
            return Ok(result);
        }

        [HttpGet("TotalDeposit")]
        public async Task<ActionResult<int>> GetTotalDepositAvailable()
        {
            _logger.LogInformation($"Buyer api called to get total deposit available");
            var result = await _buyerOperationRepository.DepoistTotalAmountAvailable(_authService.GetUserId());
            return Ok(result);
        }



        [HttpPost("Deposit")]
        public async Task<ActionResult<DepositResultDto>> AddCoinDeposit(CoinDto coinDto)
        {
            _logger.LogInformation($"Buyer api called to add coins ({coinDto.CoinType.ToString()}-{coinDto.Count})");
            var result = await _buyerOperationRepository.DepositeAmout(coinDto, _authService.GetUserId());
            return Ok(result);
        }

        [HttpPut("Reset")]
        public async Task<ActionResult<DepositResultDto>> ResetDeposit()
        {
            _logger.LogInformation($"Buyer api called to reset all deposit");
            var result = await _buyerOperationRepository.DepositeReset(_authService.GetUserId());
            return Ok(result);
        }



        [HttpPost("Buy")]
        public async Task<ActionResult<BuyerResultDto>> BuyAction(ProductToPurchaseDto productToPurchase)
        {
            _logger.LogInformation($"Buyer api called to buy the ProdcutId {productToPurchase.productId} and the amount {productToPurchase.amountOfProduct}");
            var check = await _buyerOperationRepository
                                .ValidateTheAmountOfDepoist(productToPurchase,
                                                            _authService.GetUserId());
            if (check?.Success == false)
            {
                var Out = new BuyerResultDto
                {
                    Succes = false,
                    Message = check.Message
                };
                return Ok(Out);
            }

            var result = await _buyerOperationRepository
                    .BuyProducts(productToPurchase, _authService.GetUserId());

            return Ok(result);
        }

    }
}
