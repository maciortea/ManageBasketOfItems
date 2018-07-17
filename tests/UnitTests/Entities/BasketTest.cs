using ApplicationCore.Entities;
using System.Linq;
using Xunit;

namespace UnitTests.Entities
{
    public class BasketTest
    {
        [Fact]
        public void AddItem_WithNonExistentBasketItem_ShouldAddItemToList()
        {
            // Arrange
            var basket = new Basket();

            // Act
            basket.AddItem(1, 1, Pounds.Of(1));

            // Assert
            Assert.Equal(1, basket.Items.Count);
        }

        [Fact]
        public void AddItem_WithNonExistentBasketItem_ShouldReturnNewlyCreatedItem()
        {
            // Arrange
            var basket = new Basket();

            // Act
            var item = basket.AddItem(1, 1, Pounds.Of(1));

            // Assert
            Assert.NotNull(item);
        }

        [Fact]
        public void AddItem_WithExistentBasketItem_ShouldUpdateBasketItemQuantity()
        {
            // Arrange
            var basket = new Basket();
            basket.AddItem(1, 1, Pounds.Of(1));

            // Act
            basket.AddItem(1, 1, Pounds.Of(1));

            // Assert
            var item = basket.Items.First();
            Assert.Equal(2, item.Quantity);
        }

        [Fact]
        public void RemoveItem_WithNonExistentBasketItem_ShouldReturnFailure()
        {
            // Arrange
            var basket = new Basket();

            // Act
            var result = basket.RemoveItem(1);

            // Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public void RemoveItem_WithExistentBasketItem_ShouldReturnRemoveItem()
        {
            // Arrange
            var basket = new Basket();
            basket.AddItem(1, 1, Pounds.Of(1));
            var basketItem = basket.Items.First();
            basketItem.Id = 1;

            // Act
            basket.RemoveItem(1);

            // Assert
            Assert.Equal(0, basket.Items.Count);
        }

        [Fact]
        public void RemoveItem_WithExistentBasketItem_ShouldReturnSucces()
        {
            // Arrange
            var basket = new Basket();
            basket.AddItem(1, 1, Pounds.Of(1));
            var basketItem = basket.Items.First();
            basketItem.Id = 1;

            // Act
            var result = basket.RemoveItem(1);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void Clear_ShouldClearBasketItemsList()
        {
            // Arrange
            var basket = new Basket();
            basket.AddItem(1, 1, Pounds.Of(1));
            var basketItem = basket.Items.First();
            basketItem.Id = 1;

            // Act
            basket.Clear();

            // Assert
            Assert.Equal(0, basket.Items.Count);
        }
    }
}
