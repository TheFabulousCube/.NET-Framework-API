using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Description;

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

        /// <summary>
        /// Lookup table for sleeve lengths.
        /// </summary>
        /// <remarks>
        /// ### REMARKS ###
        /// - This allows the user to search for only the
        /// specific sleeve length.
        /// - But it will **never** change and might be better
        /// as an enum.
        /// - Get and GetById are really the only useful calls for this
        /// </remarks>
        /// <returns>An array of all sleeve lengths</returns>
        /// <response code="200">Returns the list of available sleeve lengths</response>
        /// <response code="500">If there is a database error</response> 
        [HttpGet]
        [ResponseType(typeof(List<SleeveLookup>))]
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
