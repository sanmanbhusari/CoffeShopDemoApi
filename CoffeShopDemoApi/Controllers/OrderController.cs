using CoffeShopDemoApi.Model;
using CoffeShopDemoRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeShopDemoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {

        private readonly ILogger<OrderController> _logger;

        private readonly IRepository<Order> OrderRepository;

        public OrderController(ILogger<OrderController> logger, IRepository<Order> reposiotry)
        {
            _logger = logger;
            OrderRepository = reposiotry;
        }

        [HttpPost()]
        public async Task<ActionResult<Order>> Order(Order order)
        {
            try
            {

                if (order == null)
                {
                    return BadRequest();
                }

                if (order.MenuItems != null && order.MenuItems.Count() == 0)
                {
                    ModelState.AddModelError("items", "No Items found in the order");
                    return BadRequest(ModelState);
                }

                _logger.LogInformation("Inserting Order into DB");

                await OrderRepository.Insert(order);

                // Notifing user when order is ready
                //In realy senario this will be slipt into different notifications
                //1. User will be first notified that order is placed successfuly 
                //2. And then when order is ready inform users (Using Display TV/ PA System in case of coffe shop) 
                await Task.Delay(1000);//Quickest coffee shop in the world! :)

                return CreatedAtAction(nameof(Order), 0);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while processing order: Error:{0}, StackTrace:{1}", ex.Message, ex.StackTrace);
                return StatusCode(500);
            }
        }

    }
}
