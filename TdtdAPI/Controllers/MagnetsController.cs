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

        /// <summary>
        /// Gets a list of all magnets.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Magnets
        ///
        /// </remarks>
        /// <returns>An array of all State Magnets</returns>
        /// <response code="200">Returns the list of Magnets</response>
        /// <response code="500">If there is a database error</response> 
        [HttpGet]
        [ResponseType(typeof(List<Magnets>))]
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

        /// <summary>
        /// Gets a specific magnet based on it's ID
        /// </summary>
        /// <param name="id">Ids are in the format "SM" + the 2 letter state abbreviation</param>
        /// <remarks>
        /// State magnet Ids are **generally** in the format "SM" + the 2 letter state abbreviation 
        /// Sample request:
        ///
        ///     GET /Magnets/SMTN
        ///
        /// </remarks>
        /// <returns>All the information about the magnet</returns>
        /// <response code="200">Returns the magnet</response>
        /// <response code="400">If the item is null</response> 
        /// <response code="500">If there is a database error</response>    
        [HttpGet]
        [Route("api/magnets/{id}", Name ="MagnetById")]
        [ResponseType(typeof(Magnets))]
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

        /// <summary>
        /// [Restricted] Adds a new State Magnet to the product base
        /// </summary>
        /// <param name="magnet">
        /// 
        /// {
        ///     "prodId": "SMXX",
        ///     "prodPicture": "/images/SMXX.png",
        ///     "prodPrice": 1.99,
        ///     "prodQty": 10,
        ///     "catagory": "Magnets",
        ///     "prodName": "SampleStateName",
        ///     "capital": "SampleCapital",
        ///     "statehood": "June 1, 1796"
        /// }           
        /// 
        /// </param>
        /// <remarks>State magnet Ids are **generally** in the format "SM" + the 2 letter state abbreviation
        ///   Sample request:
        ///
        /// POST /Magnets
        /// {
        ///     "prodId": "SMXX",
        ///     "prodPicture": "/images/SMXX.png",
        ///     "prodPrice": 1.99,
        ///     "prodQty": 10,
        ///     "catagory": "Magnets",
        ///     "prodName": "SampleStateName",
        ///     "capital": "SampleCapital",
        ///     "statehood": "June 1, 1796"
        /// }           
        ///
        /// </remarks>
        /// <returns>All the information about the magnet</returns>
        /// <response code="201">Returns the new magnet</response>
        /// <response code="400">If the item is null or invalid</response> 
        /// <response code="500">If there is a database error</response>  
        [HttpPost]
        [ResponseType(typeof(Magnets))]
        public IHttpActionResult CreateMagnet([FromBody]Magnets magnet)
        {
            try
            {
                if (magnet == null)
                {
                    _logger.LogError("Magnet object sent from client is null.");
                    return BadRequest("Magnet object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object sent from client.");
                    return BadRequest("Invalid model object");
                }
                _repository.Magnets.CreateMagnet(magnet);
                _repository.Save();

                return CreatedAtRoute("MagnetById", new { id = magnet.ProdId }, magnet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateMagnet action: {ex.Message}");
                return InternalServerError(ex);
            }
        }
    }
}
