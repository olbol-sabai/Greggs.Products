using Greggs.Products.Api.Controllers;
using Greggs.Products.Api.Enums;
using Greggs.Products.Api.Models.DTO.Product;
using Greggs.Products.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
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
        public void ListByDateAdded_ShouldReturnBadRequest_WhenPaginationIsInvalid()
        {
            int pageStart = -1;
            int pageSize = 0;

            var result = _controller.ListByDateAdded(pageStart, pageSize);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("PageStart must be non-negative. PageSize must be greater than zero.", badRequestResult.Value);
        }
        
        [Fact]
        public void ListWithCurrency_ShouldReturnBadRequest_WhenPaginationIsInvalid()
        {
            int pageStart = 0;
            int pageSize = -1;

            var result = _controller.ListWithCurrency(Currency.GBP, pageStart, pageSize);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("PageStart must be non-negative. PageSize must be greater than zero.", badRequestResult.Value);
        }

        [Fact]
        public void ListByDateAdded_ShouldReturnOk_WhenValidPagination()
        {
            int pageStart = 0;
            int pageSize = 5;
            var products = new List<ProductDTO>
            {
                new() { Name = "Sausage Roll", PriceInPounds = 1.0m, DateAdded = DateTime.Now },
                new() { Name = "Pizza Slice", PriceInPounds = 2.0m, DateAdded = DateTime.Now }
            };

            _productServiceMock
                .Setup(service => service.List<ProductDTO>(pageStart, pageSize, nameof(ProductDTO.DateAdded)))
                .Returns(products);

            var result = _controller.ListByDateAdded(pageStart, pageSize);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProducts = Assert.IsType<List<ProductDTO>>(okResult.Value);
            Assert.Equal(2, returnedProducts.Count);
        }


        [Fact]
        public void ListWithCurrency_ShouldReturnOk_WhenValidPaginationAndResultsExist()
        {
            int pageStart = 0;
            int pageSize = 5;
            var currency = Currency.EUR;

            var products = new List<ProductWithCurrencyDTO>
            {
                new() { Name = "Sausage Roll", PriceInPounds = 1.0m, DateAdded = DateTime.Now, CurrencyCode = Currency.GBP },
                new() { Name = "Pizza Slice", PriceInPounds = 2.0m, DateAdded = DateTime.Now, CurrencyCode = Currency.GBP }
            };

            _productServiceMock
                .Setup(service => service.List<ProductWithCurrencyDTO>(pageStart, pageSize, "DateAdded"))
                .Returns(products);

            var result = _controller.ListWithCurrency(currency, pageStart, pageSize);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProducts = Assert.IsType<List<ProductWithCurrencyDTO>>(okResult.Value);

            Assert.Equal(2, returnedProducts.Count);
            Assert.All(returnedProducts, p =>
            {
                Assert.Equal(currency, p.CurrencyCode);
                Assert.Contains(products, original => original.Name == p.Name);
            });
        }

    }
}
