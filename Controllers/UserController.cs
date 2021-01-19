using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ASP.NETCoreWebApplication.Controllers
{
    public class UserController : ControllerBase
    {
        private ILogger<UserController> _logger;
        private SqlUserHelper _sqlUsersHelper;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
            _sqlUsersHelper = new SqlUserHelper("UsersContext");
        }
        
        [HttpGet]
        [Route("{id}")]
        public IEnumerable<ProductData> GetUserData(int clientId)
        {
            return null;
        }

        [HttpPost]
        [Route("/clientrole")]
        public string AddRoleToUser([FromBody] ClientRole data)
        {
            return _sqlUsersHelper.AddRole(data);
        }

        [HttpDelete]
        [Route("/clientrole")]
        public string DeleteUserFromRole([FromBody] ClientRole data)
        {
            return _sqlUsersHelper.DeleteRole(data);
        }
    }
}