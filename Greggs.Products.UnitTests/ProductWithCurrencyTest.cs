using Greggs.Products.Api.Enums;
using Greggs.Products.Api.Models.DTO.Product;
using Xunit;

namespace Greggs.Products.UnitTests;

public class ProductWithCurrencyDTOTests
{
    [Theory]
    [InlineData(1.0, Currency.GBP, 1.0)]
    [InlineData(1.0, Currency.EUR, 1.1)]
    [InlineData(1.0, Currency.USD, 1.27)]
    public void Price_ShouldConvertCorrectly(decimal priceInPounds, Currency currency, decimal expectedPrice)
    {
        var product = new ProductWithCurrencyDTO
        {
            PriceInPounds = priceInPounds,
            CurrencyCode = currency
        };

        var price = product.CurrencyAdjustedPrice;

        Assert.Equal(expectedPrice, price);
    }
}
