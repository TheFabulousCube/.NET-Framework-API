using Contracts;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace TdtdAPI.Controllers
{
    [Route("api/Roles")]
    public class RoleController : ApiController
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public RoleController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IHttpActionResult GetAllRoles()
        {
            try
            {
                var roles = _repository.RoleLookup.GetAllRoles();
                _logger.LogInfo($"{MethodBase.GetCurrentMethod().Name} returned {roles.Count()} roles in {GetType().Name} from the database");
                return Ok(roles);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {GetType().Name} action: {ex.Message}");
                return InternalServerError(ex);
            }
        }

        [HttpGet, Route("api/roles/{id}")]
        public IHttpActionResult GetRoleById(string id)
        {
            try
            {
                var role = _repository.RoleLookup.GetRoleById(id);

                if (Object.ReferenceEquals(role, null))
                {
                    _logger.LogError($"No Roles found with ID: {id}.");
                    return BadRequest($"No Roles found with ID: {id}.");
                }
                _logger.LogInfo($"{MethodBase.GetCurrentMethod().Name} returned {role.RoleId}: {role.Role} in {GetType().Name} from the database");
                return Ok(role);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the {MethodBase.GetCurrentMethod().Name} action: {ex.Message}");
                return InternalServerError(ex);
            }
        }
    }

}
