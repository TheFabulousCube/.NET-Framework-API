using Contracts;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace TdtdAPI.Controllers
{
    [Route("api/Catagories")]
    public class CatagoryController : ApiController
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public CatagoryController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IHttpActionResult GetAllCatagories()
        {
            try
            {
                var catagories = _repository.CatagoryLookup.GetAllCatagories();
                _logger.LogInfo($"{MethodBase.GetCurrentMethod().Name} returned {catagories.Count()} catagories in {GetType().Name} from the database");
                return Ok(catagories);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {GetType().Name} action: {ex.Message}");
                return InternalServerError(ex);
            }
        }

        [HttpGet, Route("api/catagories/{id}")]
        public IHttpActionResult GetCatagoryById(string id)
        {
            try
            {
                var catagory = _repository.CatagoryLookup.GetCatagoryById(id);

                if (Object.ReferenceEquals(catagory, null))
                {
                    _logger.LogError($"No Catagories found with ID: {id}.");
                    return BadRequest($"No Catagories found with ID: {id}.");
                }
                _logger.LogInfo($"{MethodBase.GetCurrentMethod().Name} returned {catagory.CatId}: {catagory.Type} in {GetType().Name} from the database");
                return Ok(catagory);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the {MethodBase.GetCurrentMethod().Name} action: {ex.Message}");
                return InternalServerError(ex);
            }
        }
    }

}
