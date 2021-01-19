using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ASP.NETCoreWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private ISqlProductsHelper _sqlProductsHelper;
        
        public ProductsController(ILogger<ProductsController> logger, ISqlProductsHelper sqlProductsHelper)
        {
            _logger = logger;
            _sqlProductsHelper = sqlProductsHelper;
        }
        
        [HttpGet]
        [Route("top")]
        public IEnumerable<ProductData> GetTop()
        {
            return _sqlProductsHelper.GetTopProducts();
        }
        
        [HttpGet]
        [Route("actual")]
        public IEnumerable<ProductData> GetActual(string queryName)
        {
            return _sqlProductsHelper.GetActualProducts();
        }
        
        [HttpGet]
        [Route("popular")]
        public IEnumerable<ProductData> GetPopular()
        {
            return _sqlProductsHelper.GetPopularProducts();
        }
    }
}