using ApplicationCore.Entities;
using Xunit;

namespace UnitTests.Entities
{
    public class PoundsTest
    {
        [Fact]
        public void Create_WithNegativeAmount_ShouldReturnFailure()
        {
            // Arrange
            var result = Pounds.Create(-1);

            // Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public void Create_WithBigAmount_ShouldReturnFailure()
        {
            // Arrange
            var result = Pounds.Create(Pounds.MaxPoundAmount + 1);

            // Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public void Create_WithPartOfPennyAmount_ShouldReturnFailure()
        {
            // Arrange
            var result = Pounds.Create(0.001m);

            // Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public void Create_WithValidAmount_ShouldReturnSuccess()
        {
            // Arrange
            var result = Pounds.Create(1);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void OperatorMultiplication_ShouldMultiplyAmounts()
        {
            // Arrange
            var pounds = Pounds.Of(2);

            // Act
            var result = pounds * 5;

            // Assert
            Assert.Equal(Pounds.Of(10), result);
        }

        [Fact]
        public void OperatorAdd_ShouldAddAmounts()
        {
            // Arrange
            var pounds1 = Pounds.Of(2);
            var pounds2 = Pounds.Of(3);

            // Act
            var result = pounds1 + pounds2;

            // Assert
            Assert.Equal(Pounds.Of(5), result);
        }

        [Fact]
        public void OperatorConvertToDecimal_ShouldConvertPoundsToDecimal()
        {
            // Arrange
            var pounds = Pounds.Of(5);

            // Act
            decimal value = pounds;

            // Assert
            Assert.Equal(5, value);
        }
    }
}
