using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using CSharpFunctionalExtensions;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Services
{
    public class BasketServiceTest
    {
        private readonly Mock<IAppLogger<BasketService>> _loggerMock;
        private readonly Mock<IBasketRepository> _basketRepositoryMock;
        private readonly IBasketService _basketService;

        public BasketServiceTest()
        {
            _loggerMock = new Mock<IAppLogger<BasketService>>();
            _basketRepositoryMock = new Mock<IBasketRepository>();
            _basketService = new BasketService(_basketRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void AddBasketAsync_ShouldCallAddInRepository()
        {
            // Arrange
            var basket = new Basket();
            _basketRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Basket>())).Returns(Task.CompletedTask);

            // Act
            _basketService.AddBasketAsync(basket);

            // Assert
            _basketRepositoryMock.Verify(x => x.AddAsync(basket), Times.Once);
        }

        [Fact]
        public void GetBasketByUserIdAsync_ShouldReturnBasket()
        {
            // Arrange
            _basketRepositoryMock.Setup(x => x.GetByUserIdAsync(It.IsAny<string>())).Returns(Task.FromResult(new Basket()));

            // Act
            Basket basket = _basketService.GetBasketByUserIdAsync("user").Result;

            // Assert
            _basketRepositoryMock.Verify(x => x.GetByUserIdAsync("user"), Times.Once);
        }

        [Fact]
        public void AddItemToBasketAsync_WithNonExistentBasket_ShouldReturnFailure()
        {
            // Arrange
            _basketRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult<Basket>(null));

            // Act
            var result = _basketService.AddItemToBasketAsync(1, 1, 1, Pounds.Of(1)).Result;

            // Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public void AddItemToBasketAsync_WithExistentBasket_ShouldCallUpdateInRepository()
        {
            // Arrange
            var basket = new Basket();
            _basketRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(basket));
            _basketRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Basket>())).Returns(Task.CompletedTask);

            // Act
            var result = _basketService.AddItemToBasketAsync(1, 1, 1, Pounds.Of(1)).Result;

            // Assert
            _basketRepositoryMock.Verify(x => x.UpdateAsync(basket), Times.Once);
        }

        [Fact]
        public void AddItemToBasketAsync_WithExistentBasket_ShouldReturnSuccess()
        {
            // Arrange
            var basket = new Basket();
            _basketRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(basket));

            // Act
            var result = _basketService.AddItemToBasketAsync(1, 1, 1, Pounds.Of(1)).Result;

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void RemoveItemFromBasketAsync_WithNonExistentBasket_ShouldReturnFailure()
        {
            // Arrange
            _basketRepositoryMock.Setup(x => x.GetByUserIdAsync(It.IsAny<string>())).Returns(Task.FromResult<Basket>(null));

            // Act
            var result = _basketService.RemoveItemFromBasketAsync("user", 1).Result;

            // Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public void RemoveItemFromBasketAsync_WithExistentBasket_ShouldCallUpdateInRepository()
        {
            // Arrange
            var basket = new Basket();
            _basketRepositoryMock.Setup(x => x.GetByUserIdAsync(It.IsAny<string>())).Returns(Task.FromResult(basket));
            _basketRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Basket>())).Returns(Task.CompletedTask);

            // Act
            var result = _basketService.RemoveItemFromBasketAsync("user", 1).Result;

            // Assert
            _basketRepositoryMock.Verify(x => x.UpdateAsync(basket), Times.Once);
        }

        [Fact]
        public void RemoveItemFromBasketAsync_WithExistentBasket_ShouldReturnSuccess()
        {
            // Arrange
            var basketMock = new Mock<Basket>();
            _basketRepositoryMock.Setup(x => x.GetByUserIdAsync(It.IsAny<string>())).Returns(Task.FromResult(basketMock.Object));
            basketMock.Setup(x => x.RemoveItem(It.IsAny<int>())).Returns(Result.Ok());

            // Act
            var result = _basketService.RemoveItemFromBasketAsync("user", 1).Result;

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void ClearAllItemsAsync_WithNonExistentBasket_ShouldReturnFailure()
        {
            // Arrange
            _basketRepositoryMock.Setup(x => x.GetByUserIdAsync(It.IsAny<string>())).Returns(Task.FromResult<Basket>(null));

            // Act
            var result = _basketService.ClearAllItemsAsync("user").Result;

            // Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public void ClearAllItemsAsync_WithExistentBasket_ShouldCallUpdateInRepository()
        {
            // Arrange
            var basket = new Basket();
            _basketRepositoryMock.Setup(x => x.GetByUserIdAsync(It.IsAny<string>())).Returns(Task.FromResult(basket));
            _basketRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Basket>())).Returns(Task.CompletedTask);

            // Act
            var result = _basketService.ClearAllItemsAsync("user").Result;

            // Assert
            _basketRepositoryMock.Verify(x => x.UpdateAsync(basket), Times.Once);
        }

        [Fact]
        public void ClearAllItemsAsync_WithExistentBasket_ShouldReturnSuccess()
        {
            // Arrange
            _basketRepositoryMock.Setup(x => x.GetByUserIdAsync(It.IsAny<string>())).Returns(Task.FromResult(new Basket()));

            // Act
            var result = _basketService.ClearAllItemsAsync("user").Result;

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void ChangeItemQuantityAsync_WithNonExistentBasket_ShouldReturnFailure()
        {
            // Arrange
            _basketRepositoryMock.Setup(x => x.GetByUserIdAsync(It.IsAny<string>())).Returns(Task.FromResult<Basket>(null));

            // Act
            var result = _basketService.ChangeItemQuantityAsync("user", 1, 1).Result;

            // Asert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public void ChangeItemQuantityAsync_WithNonExistentBasketItem_ShouldReturnFailure()
        {
            // Arrange
            _basketRepositoryMock.Setup(x => x.GetByUserIdAsync(It.IsAny<string>())).Returns(Task.FromResult(new Basket()));

            // Act
            var result = _basketService.ChangeItemQuantityAsync("user", 1, 1).Result;

            // Asert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public void ChangeItemQuantityAsync_WithExistentBasketItem_ShouldCallUpdateInRepository()
        {
            // Arrange
            var basket = new Basket();
            basket.AddItem(1, 1, Pounds.Of(1));
            var basketItem = basket.Items.First();
            basketItem.Id = 1;
            _basketRepositoryMock.Setup(x => x.GetByUserIdAsync(It.IsAny<string>())).Returns(Task.FromResult(basket));

            // Act
            var result = _basketService.ChangeItemQuantityAsync("user", 1, 2).Result;

            // Asert
            _basketRepositoryMock.Verify(x => x.UpdateAsync(basket), Times.Once);
        }
        [Fact]
        public void ChangeItemQuantityAsync_WithExistentBasketItem_ShouldReturnSuccess()
        {
            // Arrange
            var basket = new Basket();
            basket.AddItem(1, 1, Pounds.Of(1));
            var basketItem = basket.Items.First();
            basketItem.Id = 1;
            _basketRepositoryMock.Setup(x => x.GetByUserIdAsync(It.IsAny<string>())).Returns(Task.FromResult(basket));

            // Act
            var result = _basketService.ChangeItemQuantityAsync("user", 1, 2).Result;

            // Asert
            Assert.True(result.IsSuccess);
        }
    }
}
