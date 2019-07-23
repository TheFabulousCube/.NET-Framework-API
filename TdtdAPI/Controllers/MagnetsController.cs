using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        [Route("api/magnets/{id}", Name = "MagnetById")]
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
        /// [Admin] Adds a new State Magnet to the product base
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
        [Authorize]
        [ResponseType(typeof(Magnets))]
        public IHttpActionResult CreateMagnet([FromBody]Magnets magnet)
        {
            try
            {
                if (magnet == null)
                {
                    _logger.LogError($"{MethodBase.GetCurrentMethod().Name} Magnet object sent from client is null.");
                    return BadRequest("Magnet object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError($"{MethodBase.GetCurrentMethod().Name} Invalid Magnet object sent from client.");
                    return BadRequest($"Invalid model {magnet}");
                }

                Magnets newMagnet = _repository.Magnets.CreateMagnet(magnet);
                _repository.Save();

                return CreatedAtRoute("MagnetById", new { id = magnet.ProdId }, magnet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{MethodBase.GetCurrentMethod().Name} server error: {ex.Message}");
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// [Admin] Updates a State Magnet in the product base  
        /// Mainly used for updating inventory (quantity)
        /// </summary>
        /// <param name="id">
        /// The ID of the magnet you want to update
        /// </param>
        /// <param name="magnet">
        /// Magnet with same id, update the rest of the data.         
        /// 
        /// </param>
        /// <remarks>State magnet Ids are **generally** in the format "SM" + the 2 letter state abbreviation
        ///   Sample request:
        ///
        /// PUT /Magnets/SMXX
        /// {
        ///     "prodId": "SMXX",
        ///     "prodPicture": "/images/SMXX.png",
        ///     "prodPrice": 1.99,
        ///     "prodQty": 10, (update Qty to correct value)
        ///     "catagory": "Magnets",
        ///     "prodName": "SampleStateName",
        ///     "capital": "SampleCapital",
        ///     "statehood": "June 1, 1796"
        /// }           
        ///
        /// </remarks>
        /// <returns>All the information about the magnet</returns>
        /// <response code="204">Sucessful update, NoContent</response>
        /// <response code="400">If the item is null or invalid</response> 
        /// <response code="500">If there is a database error</response> 
        [HttpPut]
        [Authorize]
        [Route("api/magnets/{id}")]
        public IHttpActionResult UpdateMagnet(string id, [FromBody]Magnets magnet)
        {
            var test = magnet.IsMagnet();
            try
            {
                if (magnet == null)
                {
                    _logger.LogError($"{MethodBase.GetCurrentMethod().Name} Magnet sent from client is null.");
                    return BadRequest("Magnet is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError($"{MethodBase.GetCurrentMethod().Name} Magnet model is invalid {magnet}");
                    return BadRequest($"Invalid model {magnet}");
                }

                Magnets dbMagnet = _repository.Magnets.GetMagnetById(id);
                if (dbMagnet == null)
                {
                    _logger.LogError($"{MethodBase.GetCurrentMethod().Name} Magnet with id:{id} couldn't be found");
                    return NotFound();
                }

                _repository.Magnets.UpdateMagnet(dbMagnet, magnet);
                _repository.Save();

                // To return non-convenience codes use HttpStatusCode
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside {MethodBase.GetCurrentMethod().Name} action: {ex.Message}");
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// [Admin] Delete
        /// </summary>
        /// <param name="id">
        /// The ID of the magnet you want to delete
        /// </param>
        /// <param name="magnet">
        ///     The magnet to be deleted
        /// </param>
        /// <remarks>
        /// Currently removes the entry from the database.  
        /// It may make more sense just to set the Qty to '0', then decide to filter it out of GET all.
        /// Sample request:
        ///
        ///     DELETE /Magnets/{id}
        ///
        /// </remarks>
        /// <response code="200">If sucessful</response>
        /// <response code="400">If the item is null or invalid</response> 
        /// <response code="500">If there is a database error</response> 
        [HttpDelete]
        [Authorize]
        [Route("api/magnets/{id}")]
        public IHttpActionResult DeleteMagnet(string id, [FromBody] Magnets magnet)
        {
            try
            {
                if (magnet == null)
                {
                    _logger.LogError($"{MethodBase.GetCurrentMethod().Name} Magnet sent from client is null.");
                    return BadRequest("Magnet is null");
                }

                if (id != magnet.ProdId)
                {
                    _logger.LogError($"{MethodBase.GetCurrentMethod().Name} Magnet sent from client doesn't match id.");
                    return BadRequest($"Magnet id: {id} doesn't match magnet: {magnet}");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError($"{MethodBase.GetCurrentMethod().Name} Magnet model is invalid {magnet}");
                    return BadRequest($"Invalid model {magnet}");
                }

                Magnets dbMagnet = _repository.Magnets.GetMagnetById(id);
                if (dbMagnet == null)
                {
                    _logger.LogError($"{MethodBase.GetCurrentMethod().Name} Magnet with id:{magnet.ProdId} couldn't be found");
                    return NotFound();
                }

                _repository.Magnets.DeleteMagnet(magnet);
                _repository.Save();
                return Ok($"Removed {dbMagnet} from inventory");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
