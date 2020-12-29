using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Customers.Models;

namespace ECommerce.Api.Customers.Interfaces
{
    public interface ICustomersProvider
    {
        Task<(bool IsSuccess, IEnumerable<Customer> Customers, string Error)> GetCustomersAsync();
        Task<(bool IsSuccess, Customer Customer, string Error)> GetCustomerAsync(int id);
    }
}
