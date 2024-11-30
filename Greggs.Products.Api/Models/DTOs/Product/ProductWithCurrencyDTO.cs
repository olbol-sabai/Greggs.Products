using Greggs.Products.Api.Enums;
using System;

namespace Greggs.Products.Api.Models.DTO.Product;

public class ProductWithCurrencyDTO : ProductDTO, IProduct
{
    public Currency CurrencyCode { get; set; } = Currency.GBP;
    public string CurrencyCodeString => CurrencyCode.ToString();
    public decimal CurrencyAdjustedPrice { get => Utilities.CurrencyConversion.Convert(CurrencyCode, PriceInPounds) ;}
}
