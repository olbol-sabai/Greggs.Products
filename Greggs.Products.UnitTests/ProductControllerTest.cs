using Greggs.Products.Api.Controllers;
using Greggs.Products.Api.Enums;
using Greggs.Products.Api.Models.DTO.Product;
using Greggs.Products.Api.Services;
using Greggs.Products.Api.Shared.Exceptions;
using Greggs.Products.Api.Shared.ViewModels.PaginationFilterViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Greggs.Products.UnitTests
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductService> _productServiceMock;
        private readonly Mock<ILogger<ProductController>> _loggerMock;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _productServiceMock = new Mock<IProductService>();
            _loggerMock = new Mock<ILogger<ProductController>>();
            _controller = new ProductController(_productServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void ListByDateAdded_InvalidPaginationParameters_ThrowsParameterException()
        {
            var invalidPaginationParameters = new PaginationParameters();

            var exception = Assert.Throws<ParameterException>(() =>
            {
                invalidPaginationParameters.PageStart = -1;
            });

            Assert.Equal("PageStart must be non-negative.", exception.Message);
        }


        [Fact]
        public void ListByDateAdded_ShouldReturnOk_WhenValidPagination()
        {
            var products = new List<ProductDTO>
            {
                new() { Name = "Sausage Roll", PriceInPounds = 1.0m, DateAdded = DateTime.Now },
                new() { Name = "Pizza Slice", PriceInPounds = 2.0m, DateAdded = DateTime.Now }
            };

            int pageStart = 0;
            int pageSize = 5;

            var paginationModel = new PaginationParameters {
                PageSize = pageSize,
                PageStart = pageStart,
                OrderBy = false,
                OrderByField = nameof(ProductDTO.DateAdded)
            };

            _productServiceMock
                .Setup(service => service.List<ProductDTO>(
                    It.Is<PaginationParameters>(p =>
                        p.PageStart == paginationModel.PageStart &&
                        p.PageSize == paginationModel.PageSize &&
                        p.OrderBy == paginationModel.OrderBy &&
                        p.OrderByField == paginationModel.OrderByField)))
                .Returns(products);

            var result = _controller.List(pageStart, pageSize);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProducts = Assert.IsType<List<ProductDTO>>(okResult.Value);
            Assert.Equal(2, returnedProducts.Count());
        }


        [Fact]
        public void ListWithCurrency_ShouldReturnOk_WhenValidPaginationAndResultsExist()
        {
            var products = new List<ProductWithCurrencyDTO>
            {
                new() { Name = "Sausage Roll", PriceInPounds = 1.0m, DateAdded = DateTime.Now, CurrencyCode = Currency.GBP },
                new() { Name = "Pizza Slice", PriceInPounds = 2.0m, DateAdded = DateTime.Now, CurrencyCode = Currency.GBP }
            };

            int pageStart = 0;
            int pageSize = 5;
            var currency = Currency.EUR;

            var paginationModel = new PaginationParameters
            {
                PageSize = pageSize,
                PageStart = pageStart,
                OrderBy = true,
                OrderByField = nameof(ProductDTO.Name)
            };

            _productServiceMock
                .Setup(service => service.List<ProductWithCurrencyDTO>(
                    It.Is<PaginationParameters>(p =>
                        p.PageStart == paginationModel.PageStart &&
                        p.PageSize == paginationModel.PageSize &&
                        p.OrderBy == paginationModel.OrderBy &&
                        p.OrderByField == paginationModel.OrderByField)))
                .Returns(products);

            _productServiceMock
                .Setup(service => service.AssignCurrency(
                    currency,
                    It.IsAny<IEnumerable<ProductWithCurrencyDTO>>()))
                .Returns((Currency curr, IEnumerable<ProductWithCurrencyDTO> list) =>
                {
                    return list.Select(item =>
                    {
                        item.CurrencyCode = curr;
                        return item;
                    });
                });

            var result = _controller.ListWithCurrency(currency, pageStart, pageSize);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProducts = Assert.IsType<List<ProductWithCurrencyDTO>>(okResult.Value);

            Assert.Equal(2, returnedProducts.Count());
            Assert.All(returnedProducts, p =>
            {
                Assert.Equal(currency, p.CurrencyCode);
                Assert.Contains(products, original => original.Name == p.Name);
            });
        }



    }
}
