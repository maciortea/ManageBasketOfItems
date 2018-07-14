using ApplicationCore.Common;

namespace ApplicationCore.Entities
{
    public class Product : BaseEntity
    {
        private string _name;
        private int _productTypeId;

        public string Name
        {
            get => _name;
            set
            {
                Contract.Require(!string.IsNullOrWhiteSpace(value), "Product name is required");
                _name = value;
            }
        }

        public int ProductTypeId
        {
            get => _productTypeId;
            set
            {
                Contract.Require(value > 0, "Product type id must be greater than 0");
                _productTypeId = value;
            }
        }

        public ProductType ProductType { get; set; }
    }
}
