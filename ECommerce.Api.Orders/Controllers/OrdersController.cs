using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOrdersProvider OrdersProvider;

        public OrdersController(IOrdersProvider OrdersProvider)
        {
            this.OrdersProvider = OrdersProvider;
        }

        [HttpGet("customers/{customerId}")]
        public async Task<IActionResult> GetOrdersAsync(int customerId)
        {
            var result = await OrdersProvider.GetOrdersAsync(customerId);
            if (result.IsSuccess)
            {
                return Ok(result.Orders);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderAsync(int Id)
        {
            var result = await OrdersProvider.GetOrderAsync(Id);
            if (result.IsSuccess)
            {
                return Ok(result.Order);
            }

            return NotFound();
        }
    }
}
