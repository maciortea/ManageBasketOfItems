using ApplicationCore.Common;

namespace ApplicationCore.Entities
{
    public class ProductType : BaseEntity
    {
        public string Type { get; private set; }

        private ProductType()
        {
        }

        public ProductType(string type)
        {
            Contract.Require(!string.IsNullOrWhiteSpace(type), ErrorMessage.ProductTypeRequired);
            Type = type;
        }
    }
}
