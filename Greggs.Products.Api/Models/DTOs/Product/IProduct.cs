using System;

namespace Greggs.Products.Api.Models.DTO.Product
{
    public interface IProduct
    {
        public string Name { get; set; }
        public decimal PriceInPounds { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
