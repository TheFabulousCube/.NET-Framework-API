using Contracts;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace TdtdAPI.Controllers
{
    [Route("api/magnets")]
    public class MagnetsController : ApiController
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public MagnetsController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IHttpActionResult GetAllMagnets()
        {
            try
            {
                var magnets = _repository.Magnets.GetAllMagnets();
                _logger.LogInfo($"{MethodBase.GetCurrentMethod().Name} returned {magnets.Count()} magnets in {GetType().Name} from the database");
                return Ok(magnets);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the GetAllMagnets action: {ex.Message}");
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/magnets/{id}")]
        public IHttpActionResult GetMagnetById(string id)
        {
            try
            {
                var magnet = _repository.Magnets.GetMagnetById(id);

                if (object.ReferenceEquals(magnet, null))
                {
                    _logger.LogError($"Magnet with id: {id}, hasn't been found in db.");
                    return BadRequest($"Magnet with id: {id}, hasn't been found in db.");
                }
                _logger.LogInfo($"{MethodBase.GetCurrentMethod().Name} returned {magnet.ProdId} in {GetType().Name} from the database");
                return Ok(magnet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the {MethodBase.GetCurrentMethod().Name} action: {ex.Message}");
                return InternalServerError(ex);
            }
        }
    }
}
