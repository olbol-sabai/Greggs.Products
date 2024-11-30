using AutoMapper;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models.DTO.Product;
using Greggs.Products.Api.Models.Entities;
using System.Collections.Generic;

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
            int? pageStart,
            int? pageSize,
            string orderByDescendingField = nameof(ProductDTO.DateAdded))
            where T : IProduct
        { 
            var entities = _dataAccess.List(pageStart, pageSize, orderByDescendingField);
            return _mapper.Map<IEnumerable<T>>(entities);
        }

    }
}
