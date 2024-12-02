using Greggs.Products.Api.Enums;
using Greggs.Products.Api.Models.DTO.Product;
using Greggs.Products.Api.Services;
using Greggs.Products.Api.Shared.PaginationFilterViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Greggs.Products.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductService _productService;
    
    public ProductController(
        IProductService productService,
        ILogger<ProductController> logger
        )
    {
        _logger = logger;
        _productService = productService;
    }

    [HttpGet("fanatic")]
    public IActionResult List(int pageStart = 0, int pageSize = 5)
    {
        var paginationParameters = new PaginationParameters
        {
            PageStart = pageStart,
            PageSize = pageSize,
            OrderBy = false,
            OrderByField = nameof(ProductDTO.DateAdded)
        };

        var results = _productService.List<ProductDTO>(paginationParameters).ToList();
        
        return Ok(results);
    }

    [HttpGet("currency/{currency}")]
    public IActionResult ListWithCurrency(Currency currency, int pageStart = 0, int pageSize = 5)
    {
        var paginationParameters = new PaginationParameters
        {
            PageStart = pageStart,
            PageSize = pageSize,
            OrderBy = true,
            OrderByField = nameof(ProductDTO.Name)
        };

        var results = _productService.List<ProductWithCurrencyDTO>(paginationParameters);

        if (results.Any())
            results = _productService.AssignCurrency(currency, results).ToList();

        return Ok(results);
    }
}