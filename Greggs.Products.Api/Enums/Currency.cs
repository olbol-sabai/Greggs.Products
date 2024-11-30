using System.ComponentModel;

namespace Greggs.Products.Api.Enums
{
    public enum Currency
    {
        [Description("British Pound")]
        GBP = 0,
        [Description("Euros")]
        EUR = 1,
        [Description("Dollars")]
        USD = 2,
        /// ...
    }
}
