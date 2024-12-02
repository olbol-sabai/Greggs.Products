using Greggs.Products.Api.Enums;
using Greggs.Products.Api.Shared.Utilities;
using System;

namespace Greggs.Products.Api.Models.DTO.Product;

public class ProductWithCurrencyDTO : ProductDTO, IProduct
{
    public Currency CurrencyCode { get; set; } = Currency.GBP;
    public string CurrencyCodeString => CurrencyCode.ToString();
    public decimal CurrencyAdjustedPrice { get => CurrencyConversion.Convert(CurrencyCode, PriceInPounds) ;}
}
