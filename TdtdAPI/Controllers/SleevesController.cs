using Contracts;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace TdtdAPI.Controllers
{
    [Route("api/Sleeves")]
    public class SleeveController : ApiController
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public SleeveController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IHttpActionResult GetAllSleeves()
        {
            try
            {
                var sleeves = _repository.SleeveLookup.GetAllSleeves();
                _logger.LogInfo($"{MethodBase.GetCurrentMethod().Name} returned {sleeves.Count()} sleeves in {GetType().Name} from the database");
                return Ok(sleeves);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {GetType().Name} action: {ex.Message}");
                return InternalServerError(ex);
            }
        }

        [HttpGet, Route("api/sleeves/{id}")]
        public IHttpActionResult GetSleeveById(string id)
        {
            try
            {
                var sleeve = _repository.SleeveLookup.GetSleeveById(id);

                if (Object.ReferenceEquals(sleeve, null))
                {
                    _logger.LogError($"No Sleeves found with ID: {id}.");
                    return BadRequest($"No Sleeves found with ID: {id}.");
                }
                _logger.LogInfo($"{MethodBase.GetCurrentMethod().Name} returned {sleeve.SleeveID}: {sleeve.Sleeve_Length} in {GetType().Name} from the database");
                return Ok(sleeve);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the {MethodBase.GetCurrentMethod().Name} action: {ex.Message}");
                return InternalServerError(ex);
            }
        }
    }

}
