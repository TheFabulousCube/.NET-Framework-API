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
    [Route("api/Sizes")]
    public class SizeController : ApiController
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public SizeController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// Lookup table for clothing sizes.
        /// Usefull for offering a selection of sizes
        /// </summary>
        /// <remarks>
        /// ### REMARKS ###
        /// - This allows the user to search for only the
        /// specific sizes they want.
        /// - But it will **never** change and might be better
        /// as an enum.
        /// - Get and GetById are really the only useful calls for this
        /// </remarks>
        /// <returns>An array of all sizes, </returns>
        /// <response code="200">Returns the list of available sizes,</response>
        /// <response code="500">If there is a database error</response> 
        [HttpGet]
        [ResponseType(typeof(List<SizeLookup>))]
        public IHttpActionResult GetAllSizes()
        {
            try
            {
                var sizes = _repository.SizeLookup.GetAllSizes();
                _logger.LogInfo($"{MethodBase.GetCurrentMethod().Name} returned {sizes.Count()} sizes in {GetType().Name} from the database");
                return Ok(sizes);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {GetType().Name} action: {ex.Message}");
                return InternalServerError(ex);
            }
        }

        [HttpGet, Route("api/sizes/{id}")]
        public IHttpActionResult GetSizeById(string id)
        {
            try
            {
                var size = _repository.SizeLookup.GetSizeById(id);

                if (Object.ReferenceEquals(size, null))
                {
                    _logger.LogError($"No Sizes found with ID: {id}.");
                    return BadRequest($"No Sizes found with ID: {id}.");
                }
                _logger.LogInfo($"{MethodBase.GetCurrentMethod().Name} returned {size.SizeId}: {size.Size} in {GetType().Name} from the database");
                return Ok(size);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the {MethodBase.GetCurrentMethod().Name} action: {ex.Message}");
                return InternalServerError(ex);
            }
        }
    }

}

