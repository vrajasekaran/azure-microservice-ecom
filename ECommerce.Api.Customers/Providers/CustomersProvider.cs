using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.Api.Customers.Db;
using ECommerce.Api.Customers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Customer = ECommerce.Api.Customers.Models.Customer;

namespace ECommerce.Api.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private CustomersDbContext dbContext;
        private ILogger<CustomersProvider> logger;
        private IMapper mapper;

        public CustomersProvider(CustomersDbContext dbContext, ILogger<CustomersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }
        public async Task<(bool IsSuccess, IEnumerable<Models.Customer> Customers, string Error)> GetCustomersAsync()
        {
            try
            {
                var Customers = await dbContext.Customers.ToListAsync();
                if (Customers != null && Customers.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Models.Customer>>(Customers);
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

        public async Task<(bool IsSuccess, Customer Customer, string Error)> GetCustomerAsync(int id)
        {
            try
            {
                var Customer = await dbContext.Customers.FirstOrDefaultAsync(a => a.Id == id);
                if (Customer != null)
                {
                    var result = mapper.Map<Db.Customer, Models.Customer>(Customer);
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
            if (dbContext.Customers.Any() == false)
            {
                dbContext.Customers.Add(new Db.Customer() { Id = 1, Name="Vijay", Address = "101 Main St" });
                dbContext.Customers.Add(new Db.Customer() { Id = 2, Name = "Anitha", Address = "101 Main St" });
                dbContext.Customers.Add(new Db.Customer() { Id = 3, Name = "John Doe", Address = "101 Main St" });
                dbContext.Customers.Add(new Db.Customer() { Id = 4, Name = "Jane", Address = "101 Main St" });

                dbContext.SaveChanges();
            }

        }
    }
}
