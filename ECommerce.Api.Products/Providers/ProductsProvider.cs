using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Product = ECommerce.Api.Products.Models.Product;

namespace ECommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private ProductsDbContext dbContext;
        private ILogger<ProductsProvider> logger;
        private IMapper mapper;

        public ProductsProvider(ProductsDbContext dbContext, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }
        public async Task<(bool IsSuccess, IEnumerable<Models.Product> Products, string Error)> GetProductsAsync()
        {
            try
            {
                var products = await dbContext.Products.ToListAsync();
                if (products != null && products.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Product>, IEnumerable<Models.Product>>(products);
                    return (true, result, null);
                }

                return (false, null, "Not Found");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                logger?.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }

        public async Task<(bool IsSuccess, Product Product, string Error)> GetProductAsync(int id)
        {
            try
            {
                var product = await dbContext.Products.FirstOrDefaultAsync(a => a.Id == id);
                if (product != null)
                {
                    var result = mapper.Map<Db.Product, Models.Product>(product);
                    return (true, result, null);
                }

                return (false, null, "Not Found");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                logger?.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }

        private void SeedData()
        {
            if (dbContext.Products.Any() == false)
            {
                dbContext.Products.Add(new Db.Product() { Id = 1, Name="Keyboard", Price = new decimal(15.0), Inventory = 100});
                dbContext.Products.Add(new Db.Product() { Id = 2, Name = "Mouse", Price = new decimal(10.0), Inventory = 100 });
                dbContext.Products.Add(new Db.Product() { Id = 3, Name = "Computer", Price = new decimal(515.0), Inventory = 100 });
                dbContext.Products.Add(new Db.Product() { Id = 4, Name = "Disk", Price = new decimal(45.0), Inventory = 100 });

                dbContext.SaveChanges();
            }

        }
    }
}
