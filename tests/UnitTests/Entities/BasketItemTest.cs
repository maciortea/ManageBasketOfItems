using ApplicationCore.Common;
using ApplicationCore.Entities;
using System;
using Xunit;

namespace UnitTests.Entities
{
    public class BasketItemTest
    {
        [Fact]
        public void Constructor_WithNegativeProductId_ShouldThrowContractException()
        {
            // Arrange
            Func<BasketItem> createBasketItem = () => new BasketItem(-1, 1, Pounds.Of(1));

            // Assert
            Assert.Throws<ContractException>(createBasketItem);
        }

        [Fact]
        public void Constructor_WithNegativeQuantity_ShouldThrowContractException()
        {
            // Arrange
            Func<BasketItem> createBasketItem = () => new BasketItem(1, -1, Pounds.Of(1));

            // Assert
            Assert.Throws<ContractException>(createBasketItem);
        }

        [Fact]
        public void Constructor_WithNullPrice_ShouldThrowContractException()
        {
            // Arrange
            Func<BasketItem> createBasketItem = () => new BasketItem(1, 1, null);

            // Assert
            Assert.Throws<ContractException>(createBasketItem);
        }

        [Fact]
        public void Constructor_WithPriceZero_ShouldThrowContractException()
        {
            // Arrange
            Func<BasketItem> createBasketItem = () => new BasketItem(1, 1, Pounds.Of(0));

            // Assert
            Assert.Throws<ContractException>(createBasketItem);
        }

        [Fact]
        public void Constructor_WithValidArguments_ShouldInitializeFields()
        {
            // Arrange
            var basketItem = new BasketItem(1, 2, Pounds.Of(3));

            // Assert
            Assert.Equal(1, basketItem.ProductId);
            Assert.Equal(2, basketItem.Quantity);
            Assert.Equal(Pounds.Of(3), basketItem.PriceInPounds);
        }

        [Fact]
        public void ChangeQuantity_WithNegativeQuantity_ShouldThrowContractException()
        {
            // Arrange
            var basketItem = new BasketItem(1, 2, Pounds.Of(3));
            Action changeQuantity = () => basketItem.ChangeQuantity(-1);

            // Assert
            Assert.Throws<ContractException>(changeQuantity);
        }

        [Fact]
        public void ChangeQuantity_WithValidQuantity_ShouldChangeQuantity()
        {
            // Arrange
            var basketItem = new BasketItem(1, 2, Pounds.Of(3));

            // Act
            basketItem.ChangeQuantity(4);

            // Assert
            Assert.Equal(4, basketItem.Quantity);
        }
    }
}
