using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.Api.Orders.Db;
using ECommerce.Api.Orders.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Order = ECommerce.Api.Orders.Models.Order;

namespace ECommerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        private OrdersDbContext dbContext;
        private ILogger<OrdersProvider> logger;
        private IMapper mapper;

        public OrdersProvider(OrdersDbContext dbContext, ILogger<OrdersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }
        public async Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string Error)> GetOrdersAsync(int customerId)
        {
            try
            {
                var Orders = await dbContext.Orders.Where(a=>a.CustomerId == customerId).ToListAsync();
                if (Orders != null && Orders.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(Orders);
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

        public async Task<(bool IsSuccess, Order Order, string Error)> GetOrderAsync(int orderId)
        {
            try
            {
                var Order = await dbContext.Orders.FirstOrDefaultAsync(a => a.Id == orderId);
                if (Order != null)
                {
                    var result = mapper.Map<Db.Order, Models.Order>(Order);
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
            if (dbContext.Orders.Any() == false)
            {
                List<OrderItem>  items = new List<OrderItem>() {new OrderItem() { Id = 1, OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 100}};
                dbContext.Orders.Add(new Db.Order() { Id = 1, CustomerId = 1, OrderDate = DateTime.Today, Total  = new decimal(100), Items = items});
                
                dbContext.SaveChanges();
            }

        }
    }
}
