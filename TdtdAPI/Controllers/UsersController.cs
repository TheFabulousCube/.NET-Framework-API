using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace TdtdAPI.Controllers
{
    [Route("api/users")]
    public class UsersController : ApiController
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public UsersController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IHttpActionResult GetAllUsers()
        {
            try
            {
                var users = _repository.Users.GetAllUsers();
                _logger.LogInfo($"{MethodBase.GetCurrentMethod().Name} returned {users.Count()} users in {GetType().Name} from the database");
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the GetAllUsers action: {ex.Message}");
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/users/{id}")]
        public IHttpActionResult GetUserById(int id)
        {
            try
            {
                var user = _repository.Users.GetUserById(id);

                if (object.ReferenceEquals(user, null))
                {
                    _logger.LogError($"User with id: {id}, hasn't been found in db.");
                    return BadRequest($"User with id: {id}, hasn't been found in db.");
                }
                _logger.LogInfo($"{MethodBase.GetCurrentMethod().Name} returned {user.Id} in {GetType().Name} from the database");
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the {MethodBase.GetCurrentMethod().Name} action: {ex.Message}");
                return InternalServerError(ex);
            }
        }
    }
}
