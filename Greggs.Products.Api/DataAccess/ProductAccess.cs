using System;
using System.Collections.Generic;
using System.Linq;
using Greggs.Products.Api.Models.Entities;
using Greggs.Products.Api.Shared.PaginationFilterViewModels;

namespace Greggs.Products.Api.DataAccess;

/// <summary>
/// DISCLAIMER: This is only here to help enable the purpose of this exercise, this doesn't reflect the way we work!
/// </summary>
public class ProductAccess : IDataAccess<Product>
{
    private static readonly IEnumerable<Product> ProductDatabase = new List<Product>()
    {
        new() { Name = "Sausage Roll", PriceInPounds = 1m, DateAdded = new DateTime(1922, 1, 1) },
        new() { Name = "Vegan Sausage Roll", PriceInPounds = 1.1m, DateAdded = new DateTime(2020, 1, 1) },
        new() { Name = "Steak Bake", PriceInPounds = 1.2m, DateAdded = new DateTime(1970, 1, 1) },
        new() { Name = "Yum Yum", PriceInPounds = 0.7m, DateAdded = new DateTime(2005, 1, 1) },
        new() { Name = "Pink Jammie", PriceInPounds = 0.5m, DateAdded = new DateTime(2000, 1, 11) },
        new() { Name = "Mexican Baguette", PriceInPounds = 2.1m, DateAdded = new DateTime(2015, 1, 1) },
        new() { Name = "Bacon Sandwich", PriceInPounds = 1.95m, DateAdded = new DateTime(1950, 1, 1) },
        new() { Name = "Coca Cola", PriceInPounds = 1.2m, DateAdded = new DateTime(1920, 1, 1) }
    };


    public IEnumerable<Product> List(PaginationParameters paginationParameters)
    {
        var queryable = ProductDatabase.AsQueryable();

        var propertyInfo = typeof(Product).GetProperty(paginationParameters.OrderByField);
        if (propertyInfo != null)
        {
            if (paginationParameters.OrderBy)
            {
                queryable = queryable.OrderBy(x => propertyInfo.GetValue(x));
            }
            else
            { 
                queryable = queryable.OrderByDescending(x => propertyInfo.GetValue(x));
            }
        }
        else
        {
            if (paginationParameters.OrderBy)
            {
                queryable = queryable.OrderBy(s => s.DateAdded);
            }
            else
            { 
                queryable = queryable.OrderByDescending(s => s.DateAdded);
            }
        }

        queryable = queryable.Skip(paginationParameters.PageStart);

        queryable = queryable.Take(paginationParameters.PageSize);

        return queryable.ToList();
    }
}