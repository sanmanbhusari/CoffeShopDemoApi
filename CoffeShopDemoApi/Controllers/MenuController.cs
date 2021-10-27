using CoffeShopDemoApi.Model;
using CoffeShopDemoRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeShopDemoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MenuController : ControllerBase
    {

        private readonly ILogger<MenuController> _logger;

        private readonly IRepository<MenuItem> MenuItemRepository;

        public MenuController(ILogger<MenuController> logger, IRepository<MenuItem> reposiotry)
        {
            _logger = logger;
            MenuItemRepository = reposiotry;
        }

        [HttpGet()]
        public async Task<IEnumerable<MenuItem>> GetMenuItems()
        {
            try
            {
                _logger.LogInformation("Getting Menu items from Db");
                return await MenuItemRepository.Get();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while getting menu items: Error:{0}, StackTrace:{1}", ex.Message, ex.StackTrace);
                throw;
            }
        }


    }
}
