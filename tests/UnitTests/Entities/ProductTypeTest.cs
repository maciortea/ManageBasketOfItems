using ApplicationCore.Common;
using ApplicationCore.Entities;
using System;
using Xunit;

namespace UnitTests.Entities
{
    public class ProductTypeTest
    {
        [Fact]
        public void Constructor_WithEmptyType_ShouldThrowContractException()
        {
            // Arrange
            Func<ProductType> createProductType = () => new ProductType("");

            // Assert
            Assert.Throws<ContractException>(createProductType);
        }

        [Fact]
        public void Constructor_WithValidArguments_ShouldInitializeType()
        {
            // Arrange
            var productType = new ProductType("type");

            // Assert
            Assert.Equal("type", productType.Type);
        }
    }
}
