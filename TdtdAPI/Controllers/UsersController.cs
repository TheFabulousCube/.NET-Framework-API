using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Description;

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

        /// <summary>
        /// Gets a list of all users.
        /// </summary>
        /// <remarks>
        /// Gets a list of all users. 
        ///
        /// </remarks>
        /// <returns>An array of all users</returns>
        /// <response code="200">Returns the list of users</response>
        /// <response code="500">If there is a database error</response> 
        [HttpGet]
        [ResponseType(typeof(List<Users>))]
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

        /// <summary>
        /// Gets a specific user based on their ID
        /// </summary>
        /// <param name="id">Ids are the PK from the Users table</param>
        /// <remarks>
        /// Getting a user by ID isn't really useful
        /// Use the GET /Users/{string userName} call instead
        /// Sample request:
        ///
        ///     GET /Users/2
        ///
        /// </remarks>
        /// <returns>All the information about the user</returns>
        /// <response code="200">Returns the user</response>
        /// <response code="400">If the item is null</response> 
        /// <response code="500">If there is a database error</response>  
        [HttpGet]
        [Route("api/users{id}")]
        [ResponseType(typeof(Users))]
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

        /// <summary>
        /// Gets a specific user based on their user name
        /// </summary>
        /// <param name="userName">User name of the user</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Users/{string: userName}
        ///
        /// </remarks>
        /// <returns>All the information about the user</returns>
        /// <response code="200">Returns the user</response>
        /// <response code="400">If the item is null</response> 
        /// <response code="500">If there is a database error</response>  
        [HttpGet]
        [Route("api/users/{userName}")]
        [ResponseType(typeof(Users))]
        public IHttpActionResult GetUserByUserName(string userName)
        {
            try
            {
                var user = _repository.Users.GetUserByUserName(userName);

                if (object.ReferenceEquals(user, null))
                {
                    _logger.LogError($"User with user name: {userName}, hasn't been found in db.");
                    return BadRequest($"User with user name: {userName}, hasn't been found in db.");
                }
                _logger.LogInfo($"{MethodBase.GetCurrentMethod().Name} returned {user.Username} in {GetType().Name} from the database");
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
