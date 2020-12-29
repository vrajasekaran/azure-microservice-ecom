using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Products.Models;

namespace ECommerce.Api.Products.Interfaces
{
    public interface IProductsProvider
    {
        Task<(bool IsSuccess, IEnumerable<Product> Products, string Error)> GetProductsAsync();
        Task<(bool IsSuccess, Product Product, string Error)> GetProductAsync(int id);
    }
}
