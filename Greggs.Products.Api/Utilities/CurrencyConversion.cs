using Greggs.Products.Api.Enums;
using System;

namespace Greggs.Products.Api.Utilities
{
    public static class CurrencyConversion
    {
        public const decimal EuroConversion = 1.1M;
        public const decimal DollarConversion = 1.27M;

        public static decimal Convert(Currency currency, decimal amountInPounds)
        {
            switch (currency)
            {
                case Currency.GBP:
                    return amountInPounds;
                case Currency.EUR:
                    return amountInPounds * EuroConversion;
                case Currency.USD:
                    return amountInPounds * DollarConversion;
                default:
                    throw new ArgumentException("Currency not found");
            }
        }
    }
}
