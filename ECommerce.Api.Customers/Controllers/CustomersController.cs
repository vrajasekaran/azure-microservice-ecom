using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Customers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private ICustomersProvider CustomersProvider;

        public CustomersController(ICustomersProvider CustomersProvider)
        {
            this.CustomersProvider = CustomersProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var result = await CustomersProvider.GetCustomersAsync();
            if (result.IsSuccess)
            {
                return Ok(result.Customers);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerAsync(int Id)
        {
            var result = await CustomersProvider.GetCustomerAsync(Id);
            if (result.IsSuccess)
            {
                return Ok(result.Customer);
            }

            return NotFound();
        }
    }
}
