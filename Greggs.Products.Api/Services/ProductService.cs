using AutoMapper;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Enums;
using Greggs.Products.Api.Models.DTO.Product;
using Greggs.Products.Api.Models.Entities;
using Greggs.Products.Api.Shared.ViewModels.PaginationFilterViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Greggs.Products.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IDataAccess<Product> _dataAccess;
        private readonly IMapper _mapper;

        public ProductService(
            IDataAccess<Product> dataAccess,
            IMapper mapper
            )
        {
            _dataAccess = dataAccess;
            _mapper = mapper;
        }

        public IEnumerable<T> List<T>(
            PaginationParameters paginationParameters)
            where T : IProduct
        { 
            var entities = _dataAccess.List(paginationParameters);
            return _mapper.Map<IEnumerable<T>>(entities);
        }
        
        public IEnumerable<ProductWithCurrencyDTO> AssignCurrency(
            Currency currency,
            IEnumerable<ProductWithCurrencyDTO> list)
        { 
            return list.Select(item =>
            {
                item.CurrencyCode = currency;
                return item;
            })
            .ToList();
        }

    }
}
