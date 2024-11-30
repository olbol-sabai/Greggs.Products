using Greggs.Products.Api.Models.DTO.Product;
using System.Collections.Generic;

namespace Greggs.Products.Api.Services
{
    public interface IProductService
    {
        IEnumerable<T> List<T>(int? pageStart, int? pageSize, string orderByDescendingField = nameof(ProductDTO.DateAdded)) where T : IProduct;
    }
}