using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VendingMachine.Database;
using VendingMachine.Models;
using VendingMachine.Shared;
using VendingMachine.Shared.Dto;
using VendingMachine.WebAPI.Services.ProductService;

namespace VendingMachine.WebAPI.Services.BuyerOperation
{
    public class BuyerOperationRepository : IBuyerOperationRepository
    {
        private readonly ILogger<BuyerOperationRepository> _logger;
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public BuyerOperationRepository(
                         ILogger<BuyerOperationRepository> logger,
                         DataContext dbContext,
                         IMapper mapper,
                         IProductRepository productRepository)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
            _productRepository = productRepository;
        }



        public async Task<DepositResultDto> DepositeAmout(CoinDto coinDto, int userId)
        {
            var user = await _dbContext.Users.Include(u => u.Deposit).ThenInclude(c => c.Coins).FirstAsync(u => u.Id == userId);
            if (user.Deposit == null)
            {
                user.Deposit = new Deposit { Coins = new List<Coin>() };
            }
            var Coin = user.Deposit.Coins!.FirstOrDefault(c => c.CoinType == coinDto.CoinType);
            if (Coin == null)
            {
                Coin = new Coin() { CoinType = coinDto.CoinType };
                user.Deposit.Coins!.Add(Coin);
            }
            Coin.Count += coinDto.Count;
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<DepositResultDto>(user.Deposit);
        }
        public async Task<int> DepoistTotalAmountAvailable(int userId)
        {
            var user = await _dbContext.Users.Include(u => u.Deposit).ThenInclude(c => c.Coins).FirstAsync(u => u.Id == userId);
            if (user.Deposit == null || user.Deposit.Coins == null || user.Deposit.Coins.Count == 0)
            {
                return 0;
            }
            return user.Deposit.Coins.Sum(c => c.Count * (int)c.CoinType);
        }
        public async Task<DepositResultDto> DepositeReset(int userId)
        {
            var user = await _dbContext.Users.Include(u => u.Deposit).ThenInclude(c => c.Coins).FirstAsync(u => u.Id == userId);
            var userDepoist = user.Deposit;
            if (userDepoist == null || userDepoist.Coins == null || userDepoist.Coins.Count == 0)
            {
                return _mapper.Map<DepositResultDto>(userDepoist);
            }
            foreach (var Onecoin in userDepoist.Coins!)
            {
                Onecoin.Count = 0;
            }
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<DepositResultDto>(userDepoist);
        }
        public async Task<IEnumerable<CustomerProductDto>?> GetCustomerProductHistory(int userId)
        {
            var Lst = await _dbContext.CustomerProducts
                                      .Where(c => c.UserId == userId)
                                      .OrderByDescending(o => o.BoughtOn)
                                      .Take(50)
                                      .ToListAsync();
            return _mapper.Map<List<CustomerProductDto>>(Lst);
        }

        public async Task<DepositDto?> DepoistAvailable(int userId)
        {
            var user = await _dbContext.Users.Include(u => u.Deposit).ThenInclude(c => c.Coins).FirstAsync(u => u.Id == userId);
            var userDepoist = user.Deposit;
            if (userDepoist == null || userDepoist.Coins == null)
            {
                return null;
            }
            return _mapper.Map<DepositDto>(userDepoist);
        }

        public async Task<ServiceResponse<bool>> ValidateTheAmountOfDepoist(ProductToPurchaseDto productToBuy, int userId)
        {
            int productId = productToBuy.productId;
            int amountOfProduct = productToBuy.amountOfProduct;
            var result = new ServiceResponse<bool>();
            var product = await _productRepository.GetProductAsync(productId);
            if (product == null)
            {
                result.Success = false;
                result.Data = false;
                result.Message = "Product Not available";
                return result;
            }
            if (amountOfProduct > product.AmountAvailable)
            {
                result.Success = false;
                result.Data = false;
                result.Message = "No available amount of this product!";
                return result;
            }
            var user = await _dbContext.Users.Include(u => u.Deposit).ThenInclude(c => c.Coins).FirstAsync(u => u.Id == userId);
            var TotalAmountOfDeposit = user.Deposit?.Coins?.Sum(c => (int)c.CoinType * c.Count);
            if (TotalAmountOfDeposit == null || TotalAmountOfDeposit.Value < amountOfProduct * product.Cost)
            {
                result.Success = false;
                result.Data = false;
                result.Message = "No available deposit in your account!";
                return result;
            }
            return result;
        }

        public async Task<BuyerResultDto> BuyProducts(ProductToPurchaseDto productToBuy, int userId)
        {
            var user = await _dbContext.Users.Include(u => u.Deposit).ThenInclude(c => c.Coins).FirstAsync(u => u.Id == userId);
            var product = await _dbContext.Products.FindAsync(productToBuy.productId);
            product!.AmountAvailable -= productToBuy.amountOfProduct;
            var TotalCostOfProduct = product.Cost * productToBuy.amountOfProduct;
            UpdateTheCoinIfAnyOneMissing(user.Deposit!.Coins);
            var AmountSpend = UpdateDepositCoins(user.Deposit!.Coins, TotalCostOfProduct);

            var customerProduct = _mapper.Map<CustomerProduct>(product);
            customerProduct.AmountOfProduct = productToBuy.amountOfProduct;
            customerProduct.ProductId = product.Id;
            customerProduct.UserId = user.Id;
            _dbContext.CustomerProducts.Add(customerProduct);
            await _dbContext.SaveChangesAsync();
            var result = new BuyerResultDto
            {
                AmountSpend = AmountSpend,
                User = _mapper.Map<UserDto>(user),
                productPurchased = _mapper.Map<ProductPurchasedDto>(product)
            };
            return result;
        }


        // OLD Method 
        private int UpdateDepositCoinsWithReDistrubuteTheCoinsValue(ICollection<Coin>? Coins, double Cost)
        {
            if (Coins == null || Coins.Count == 0) throw new Exception("No available deposit in your account!"); ;
            var TotalDeposit = Coins.Sum(c => (int)c.CoinType * c.Count);
            if (Cost > TotalDeposit) throw new Exception("No available deposit in your account!");
            var NewTotalDeposit = TotalDeposit - Cost;
            foreach (var OneCoin in Coins.OrderByDescending(c => c.CoinType))
            {
                var floatNumber = NewTotalDeposit / (double)OneCoin.CoinType;
                OneCoin.Count = (int)Math.Truncate(floatNumber);
                NewTotalDeposit -= OneCoin.Count * (int)OneCoin.CoinType;
            }
            var NewTotalDepositAfterBought = Coins.Sum(c => (int)c.CoinType * c.Count);
            return TotalDeposit - NewTotalDepositAfterBought;
        }


        private void UpdateTheCoinIfAnyOneMissing(ICollection<Coin>? Coins)
        {
            if (Coins == null) Coins = new List<Coin>();    
            foreach (var coinType in Enum.GetValues(typeof(CoinTypeEnum)).Cast<CoinTypeEnum>())
            {
                if (!Coins.Any(c => c.CoinType == coinType))
                {
                    Coins.Add(new Coin { CoinType = coinType });
                }
            }
        }

        public int UpdateDepositCoins(ICollection<Coin>? Coins, double Cost)
        {
            if (Coins == null || Coins.Count == 0) throw new Exception("No available deposit in your account!"); ;
            var TotalDeposit = Coins.Sum(c => (int)c.CoinType * c.Count);
            if (Cost > TotalDeposit) throw new Exception("No available deposit in your account!");
            UpdateDepositCoinsAndManageTheChange(Coins, Cost);
            var NewTotalDepositAfterBought = Coins.Sum(c => (int)c.CoinType * c.Count);
            return TotalDeposit - NewTotalDepositAfterBought;
        }
        private void UpdateDepositCoinsAndManageTheChange(ICollection<Coin> Coins, double Cost)
        {
            var firstCoinGreatOrEqualTotheCost = Coins.OrderBy(c => c.CoinType).FirstOrDefault(c => (int)c.CoinType >= Cost && c.Count > 0);
            if (firstCoinGreatOrEqualTotheCost == null)
            {
                firstCoinGreatOrEqualTotheCost = Coins.OrderBy(c => c.CoinType).Last(c => c.Count > 0);
            }
            firstCoinGreatOrEqualTotheCost.Count--;
            var NewCost = Cost - (int)firstCoinGreatOrEqualTotheCost.CoinType;
            if (NewCost <= -5)
            {
                AddTheChangeToDeposit(Coins, Math.Abs(NewCost));
                return;
            }
            if (NewCost <= 0) return;
            UpdateDepositCoinsAndManageTheChange(Coins, NewCost);
        }
        private void AddTheChangeToDeposit(ICollection<Coin> Coins, double RemainCost)
        {
            foreach (var OneCoin in Coins.OrderByDescending(c => c.CoinType))
            {
                var floatNumber = RemainCost / (double)OneCoin.CoinType;
                OneCoin.Count += (int)Math.Truncate(floatNumber);
                RemainCost -= (int)Math.Truncate(floatNumber) * (int)OneCoin.CoinType;
            }
        }

    }
}
