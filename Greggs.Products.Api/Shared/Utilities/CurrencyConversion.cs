using Greggs.Products.Api.Enums;
using System;

namespace Greggs.Products.Api.Shared.Utilities
{
    public static class CurrencyConversion
    {
        public const decimal EuroConversion = 1.1M;
        public const decimal DollarConversion = 1.27M;

        public static decimal Convert(Currency currency, decimal amountInPounds)
        {
            decimal convertedAmount;

            switch (currency)
            {
                case Currency.GBP:
                    convertedAmount = amountInPounds;
                    break;
                case Currency.EUR:
                    convertedAmount = amountInPounds * EuroConversion;
                    break;
                case Currency.USD:
                    convertedAmount = amountInPounds * DollarConversion;
                    break;
                default:
                    throw new ArgumentException("Currency not found");
            }

            return decimal.Round(convertedAmount, 2, MidpointRounding.AwayFromZero);
        }
    }
}
