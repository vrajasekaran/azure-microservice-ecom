using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Orders.Models;

namespace ECommerce.Api.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        Task<(bool IsSuccess, IEnumerable<Order> Orders, string Error)> GetOrdersAsync(int customerId);
        Task<(bool IsSuccess, Order Order, string Error)> GetOrderAsync(int id);
    }
}
    