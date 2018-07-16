using ApplicationCore.Common;
using ApplicationCore.Entities;
using System;
using Xunit;

namespace UnitTests.Entities
{
    public class ProductTest
    {
        [Fact]
        public void Constructor_WithEmptyName_ShouldThrowContractException()
        {
            // Arrange
            Func<Product> createProduct = () => new Product("", 1);

            // Assert
            Assert.Throws<ContractException>(createProduct);
        }

        [Fact]
        public void Constructor_WithNegativeProductTypeId_ShouldThrowContractException()
        {
            // Arrange
            Func<Product> createProduct = () => new Product("name", -1);

            // Assert
            Assert.Throws<ContractException>(createProduct);
        }

        [Fact]
        public void Constructor_WithValidArguments_ShouldInitializeFields()
        {
            // Arrange
            var product = new Product("name", 1);

            // Assert
            Assert.Equal("name", product.Name);
            Assert.Equal(1, product.ProductTypeId);
        }
    }
}
