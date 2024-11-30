using Greggs.Products.Api.Enums;
using Greggs.Products.Api.Models.DTO.Product;
using Greggs.Products.Api.Services;
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

    private const string InvalidPaginationParameterMessage = "PageStart must be non-negative. PageSize must be greater than zero.";

    [HttpGet("fanatic")]
    public IActionResult ListByDateAdded(int pageStart = 0, int pageSize = 5)
    {
        if (pageStart < 0 || pageSize <= 0)
        {
            return BadRequest(InvalidPaginationParameterMessage);
        }
        var results = _productService.List<ProductDTO>(pageStart, pageSize, nameof(ProductDTO.DateAdded));
        
        return Ok(results);
    }

    [HttpGet("currency/{currency}")]
    public IActionResult ListWithCurrency(Currency currency, int pageStart = 0, int pageSize = 5)
    {
        if (pageStart < 0 || pageSize <= 0)
        {
            return BadRequest(InvalidPaginationParameterMessage);
        }

        var results = _productService.List<ProductWithCurrencyDTO>(pageStart, pageSize);
        
        if (results.Any())
            results = _productService
                .List<ProductWithCurrencyDTO>(pageStart, pageSize)
                .Select(item =>
                {
                    item.CurrencyCode = currency;
                    return item;
                })
                .ToList();

        return Ok(results);
    }
}