using VendingMachine.Models;
using VendingMachine.Shared;
using VendingMachine.WebAPI.Services.BuyerOperation;
using Xunit.Abstractions;

namespace VendingMachine.API
{
    public class BuyOperationTest
    {
        BuyerOperationRepository _buyerOperationRepository;


        private readonly ITestOutputHelper output;

        public BuyOperationTest(ITestOutputHelper output)
        {
            _buyerOperationRepository = new BuyerOperationRepository(null, null, null, null);
            this.output = output;
        }



        [Theory]
        [InlineData(new int[] { 2, 2, 2, 2, 1 }, 149.99, new int[] { 2, 2, 2, 1, 0 }, 150)]
        [InlineData(new int[] { 0, 0, 2, 0, 0 }, 29.99, new int[] { 0, 1, 0, 0, 0 }, 30)]
        [InlineData(new int[] { 0, 0, 0, 1, 0 }, 14.99, new int[] { 1, 1, 1, 0, 0 }, 15)]
        [InlineData(new int[] { 0, 0, 2, 5, 0 }, 288, new int[] { 0, 0, 0, 0, 0 }, 290)]
        [InlineData(new int[] { 0, 0, 0, 5, 0 }, 107, new int[] { 0, 0, 2, 2, 0 }, 110)]
        [InlineData(new int[] { 0, 0, 0, 5, 1 }, 284.99, new int[] { 1, 1, 0, 1, 0 }, 285)]
        [InlineData(new int[] { 0, 0, 3, 1, 0 }, 97, new int[] { 0, 1, 0, 0, 0 }, 100)]
        [InlineData(new int[] { 2, 0, 2, 0, 1 }, 14.99, new int[] { 3, 0, 1, 0, 1 }, 15)]
        [InlineData(new int[] { 0, 0, 0, 0, 1 }, 4.99, new int[] { 1, 0, 2, 1, 0 }, 5)]
        [InlineData(new int[] { 1, 1, 1, 1, 1 }, 184.99, new int[] { 0, 0, 0, 0, 0 },185)]
        public void UpdateDepositCoinsOperationTest(int[] originalCoins, double cost, int[] remainCoins, int productCost)
        {
            //Arrange 
            var actual = GetListCoin(originalCoins);
            var expected = GetListCoin(remainCoins);

            //Act 
            var costExpected = _buyerOperationRepository.UpdateDepositCoins(actual, cost);

            //Assert 
            output.WriteLine($"Expected Coins..{string.Join("-", expected.Select(c => c.Count))}");
            output.WriteLine($"Actual Coins..{string.Join("-", actual.Select(c => c.Count))}");
            Assert.Equal(expected[0].Count, actual[0].Count);
            Assert.Equal(expected[1].Count, actual[1].Count);
            Assert.Equal(expected[2].Count, actual[2].Count);
            Assert.Equal(expected[3].Count, actual[3].Count);
            Assert.Equal(expected[4].Count, actual[4].Count);
            Assert.Equal(costExpected, productCost);

        }



        private List<Coin> GetListCoin(int[] coin)
        {
            return new List<Coin>()
            {
                new Coin { CoinType = CoinTypeEnum.Coin5Cent,Count = coin[0]},
                new Coin { CoinType = CoinTypeEnum.Coin10Cent,Count = coin[1]},
                new Coin { CoinType = CoinTypeEnum.Coin20Cent,Count = coin[2]},
                new Coin { CoinType = CoinTypeEnum.Coin50Cent,Count = coin[3]},
                new Coin { CoinType = CoinTypeEnum.Coin100Cent,Count = coin[4]},
            };
        }
    }
}