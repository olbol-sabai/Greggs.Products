using Greggs.Products.Api.Enums;
using Greggs.Products.Api.Models.DTO.Product;
using Greggs.Products.Api.Shared.PaginationFilterViewModels;
using System.Collections.Generic;

namespace Greggs.Products.Api.Services
{
    public interface IProductService
    {
        IEnumerable<ProductWithCurrencyDTO> AssignCurrency(Currency currency, IEnumerable<ProductWithCurrencyDTO> list);
        IEnumerable<T> List<T>(PaginationParameters paginationParameters) where T : IProduct;
    }
}