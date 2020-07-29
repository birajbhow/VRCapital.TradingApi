using Moq;
using NUnit.Framework;
using vr.mock.api.DomainObjects;
using vr.mock.api.Dtos;
using vr.mock.api.Services;
using VRTradingInfrastructureServices;

namespace vr.mock.api.tests
{
    public class StrategyRepositoryTests
    {
        private StrategyRepository subject;
        private Mock<IStrategyTradingService> mockStrategyTradingService;
        private Mock<ILocalCache> mockLocalCache;

        [SetUp]
        public void Setup()
        {
            this.mockStrategyTradingService = new Mock<IStrategyTradingService>();
            this.mockLocalCache = new Mock<ILocalCache>();

            this.subject = new StrategyRepository(this.mockStrategyTradingService.Object,
                this.mockLocalCache.Object);
        }

        [Test]
        public void RegisterStrategy_works_as_expected()
        {
            // arrange
            var dto = new StrategyDetailsDto()
            {
                Ticker = "TESLA",
                Instruction = BuySell.Buy,
                PriceMovement = 5,
                Quantity = 100
            };

            this.mockStrategyTradingService
                .Setup(x => x.GetLiveQuote(It.IsAny<string>()))
                .Returns(200)
                .Verifiable();

            this.mockLocalCache
                .Setup(x => x.Put<Strategy>(It.IsAny<string>(), It.IsAny<Strategy>()))
                .Verifiable();

            // act
            var result = this.subject.RegisterStrategy(dto);

            // assert
            this.mockStrategyTradingService
                .Verify(x => x.GetLiveQuote(It.IsAny<string>()));
            this.mockLocalCache
                .Verify(x => x.Put<Strategy>(It.IsAny<string>(), It.IsAny<Strategy>()));

            Assert.IsNotNull(result);
        }

        [Test]
        public void RegisterStrategy_returns_null_if_live_quote_is_null()
        {
            // arrange
            var dto = new StrategyDetailsDto()
            {
                Ticker = "TESLA",
                Instruction = BuySell.Buy,
                PriceMovement = 5,
                Quantity = 100
            };

            this.mockStrategyTradingService
                .Setup(x => x.GetLiveQuote(It.IsAny<string>()))
                .Returns((decimal?) null)
                .Verifiable();

            // act
            var result = this.subject.RegisterStrategy(dto);

            // assert
            this.mockStrategyTradingService
                .Verify(x => x.GetLiveQuote(It.IsAny<string>()));
            
            Assert.IsNull(result);
        }

        // TODO: More tests to go here
    }
}