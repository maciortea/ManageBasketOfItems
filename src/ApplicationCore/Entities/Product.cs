using ApplicationCore.Common;

namespace ApplicationCore.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; private set; }
        public int ProductTypeId { get; private set; }
        public ProductType ProductType { get; private set; }

        private Product()
        {
        }

        public Product(string name, int productTypeId)
        {
            Contract.Require(!string.IsNullOrWhiteSpace(name), "Product name is required");
            Contract.Require(productTypeId > 0, "Product type id must be greater than 0");

            Name = name;
            ProductTypeId = productTypeId;
        }
    }
}
