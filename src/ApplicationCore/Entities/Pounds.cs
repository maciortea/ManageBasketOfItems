using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace ApplicationCore.Entities
{
    public class Pounds : ValueObject
    {
        private const decimal MaxPoundAmount = 1_000_000;

        public decimal Value { get; }

        public bool IsZero => Value == 0;

        private Pounds(decimal value)
        {
            Value = value;
        }

        public static Result<Pounds> Create(decimal poundAmount)
        {
            if (poundAmount < 0)
            {
                return Result.Fail<Pounds>("Pound amount cannot be negative");
            }

            if (poundAmount > MaxPoundAmount)
            {
                return Result.Fail<Pounds>("Pound amount cannot be greater than " + MaxPoundAmount);
            }

            if (poundAmount % 0.01m > 0)
            {
                return Result.Fail<Pounds>("Pound amount cannot contain part of a penny");
            }

            return Result.Ok(new Pounds(poundAmount));
        }

        public static Pounds Of(decimal poundAmount)
        {
            return Create(poundAmount).Value;
        }

        public static Pounds operator *(Pounds pounds, decimal multiplier)
        {
            return new Pounds(pounds.Value * multiplier);
        }

        public static Pounds operator +(Pounds pounds1, Pounds pounds2)
        {
            return new Pounds(pounds1.Value + pounds2.Value);
        }

        public static implicit operator decimal(Pounds pounds)
        {
            return pounds.Value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
