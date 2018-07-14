namespace ApplicationCore.Common
{
    public static class ErrorMessage
    {
        public const string BasketWithIdDoesntExists = "Basket with id {0} doesn't exists";
        public const string UserDoesntHaveBasket = "User with id {0} doesn't have a basket";
        public const string BasketWithItemIdDoesntExists = "Basket item with id {0} doesn't exists";
        public const string ProductIdGreaterThanZero = "Product id must be greater than 0";
        public const string QuantityGreaterThanZero = "Quantity must be greater than 0";
        public const string PriceRequired = "Price is required";
        public const string PriceCannotBeZero = "Price cannot be 0";
        public const string AmountCanotBeNegative = "Pound amount cannot be negative";
        public const string AmountCannotBeGreaterThan = "Pound amount cannot be greater than {0}";
        public const string AmountCannotBePartOfPenny = "Pound amount cannot contain part of a penny";
    }
}
