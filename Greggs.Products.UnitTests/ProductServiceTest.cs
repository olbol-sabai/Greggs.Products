using AutoMapper;
using Greggs.Products.Api.Models.DTO.Product;
using Greggs.Products.Api.Models.Entities;
using Greggs.Products.Api.Services;
using Greggs.Products.Api.Shared.PaginationFilterViewModels;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Greggs.Products.UnitTests
{
    public class ProductServiceTests
    {
        [Fact]
        public void List_ShouldNotThrow_WhenInvalidOrderByFieldIsPassed()
        {
            var mockRepository = new Mock<Api.DataAccess.IDataAccess<Product>>();
            var mockMapper = new Mock<IMapper>();
            var mockService = new ProductService(mockRepository.Object, mockMapper.Object);

            var mockEntityData = new List<Product>
            {
                new() { Name = "Sausage Roll", PriceInPounds = 1.0m, DateAdded = DateTime.Now.AddDays(-1) },
                new() { Name = "Pizza Slice", PriceInPounds = 2.0m, DateAdded = DateTime.Now }
            };

            mockRepository.Setup(repo => repo.List(
                It.IsAny<PaginationParameters>()))
                .Returns(mockEntityData);

            mockMapper.Setup(mapper => mapper.Map<ProductDTO>(It.IsAny<Product>()))
                .Returns((Product product) => new ProductDTO
                {
                    Name = product.Name,
                    PriceInPounds = product.PriceInPounds,
                    DateAdded = product.DateAdded
                });

            var exception = Record.Exception(() =>
            {
                var result = mockService.List<ProductDTO>(
                    new PaginationParameters { 
                        PageSize = 5,
                        PageStart = 0,
                        OrderBy = false,
                        OrderByField = "InvalidFieldName"
                    });
            });

            Assert.Null(exception);
        }
    }
}
