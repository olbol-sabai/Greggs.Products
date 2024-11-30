using System;

namespace Greggs.Products.Api.Models.Entities;

public class Product
{
    public string Name { get; set; }
    public decimal PriceInPounds { get; set; }
    public DateTime DateAdded { get; set; }
}