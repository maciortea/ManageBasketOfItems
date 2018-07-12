using ApplicationCore.Common;

namespace ApplicationCore.Entities
{
    public class ProductType : BaseEntity
    {
        private string _type;
        public string Type
        {
            get => _type;
            set
            {
                Contract.Require(!string.IsNullOrWhiteSpace(value), "Product type is required");
                _type = value;
            }
        }
    }
}
