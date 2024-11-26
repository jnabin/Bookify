using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Shared
{
    public record Money(decimal amount, Currency Currency)
    {
        public static Money operator +(Money first, Money second)
        {
            if (first.Currency != second.Currency)
            {
                throw new InvalidOperationException("Currency have to be same");
            }

            return new Money(first.amount + second.amount, first.Currency);
        }

        public static Money zero() => new(0, Currency.None);
        public static Money zero(Currency currency) => new(0, currency);

        public bool IsZero() => this == zero(Currency);
    }
}
