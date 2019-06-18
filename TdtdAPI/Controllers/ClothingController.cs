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
    [Route("api/clothing")]
    public class ClothingController : ApiController
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public ClothingController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IHttpActionResult GetAllClothing()
        {
            try
            {
                var clothing = _repository.Clothing.GetAllClothing();
                _logger.LogInfo($"{MethodBase.GetCurrentMethod().Name} returned {clothing.Count()} clothing in {GetType().Name} from the database");
                return Ok(clothing);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the GetAllClothing action: {ex.Message}");
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/clothing/{id}")]
        public IHttpActionResult GetClothingById(string id)
        {
            try
            {
                var clothing = _repository.Clothing.GetClothingById(id);

                if (object.ReferenceEquals(clothing, null))
                {
                    _logger.LogError($"Clothing with id: {id}, hasn't been found in db.");
                    return BadRequest($"Clothing with id: {id}, hasn't been found in db.");
                }
                _logger.LogInfo($"{MethodBase.GetCurrentMethod().Name} returned {clothing.ProdId} in {GetType().Name} from the database");
                return Ok(clothing);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the {MethodBase.GetCurrentMethod().Name} action: {ex.Message}");
                return InternalServerError(ex);
            }
        }
    }
}
