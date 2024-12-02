using Greggs.Products.Api.Models.Entities;
using Greggs.Products.Api.Shared.PaginationFilterViewModels;
using System.Collections.Generic;

namespace Greggs.Products.Api.DataAccess;

public interface IDataAccess<out T>
{
    IEnumerable<Product> List(PaginationParameters paginationParameters);
}