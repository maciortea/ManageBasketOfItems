using ApplicationCore.Common;

namespace ApplicationCore.Entities
{
    public abstract class BaseEntity
    {
        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                Contract.Require(value > 0, ErrorMessage.IdGreaterThanZero);
                _id = value;
            }
        }
    }
}
