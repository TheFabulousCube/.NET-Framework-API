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

        /// <summary>
        /// Gets a list of all clothing.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /clothing
        ///
        /// </remarks>
        /// <returns>An array of all Tie Dyed clothing</returns>
        /// <response code="200">Returns an unfiltered list of Clothing</response>
        /// <response code="500">If there is a database error</response> 
        [HttpGet]
        [ResponseType(typeof(List<Clothing>))]
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

        /// <summary>
        /// Gets a specific clothing item based on it's ID
        /// </summary>
        /// <param name="id">Ids are 4 digit numbers and **always** odd</param>
        /// <remarks>
        /// Clothing Ids are coded into broad catagories:
        ///  *  2001s - Youth Ts
        ///  *  4001s - Onesies
        ///  *  6001s - Adult Ts
        ///  *  9001s - Camisoles  
        /// They are **always** odd numbered and __tend__ to go from smallest to largest ie: 
        ///     *  6001-6199 are Adult Small
        ///     *  6201-6399 are Adult Medium
        ///     *  6401-6599 are Adult Large
        ///     *  6601-6799 are Adult X-Large
        ///     *  6801-6999 are Adult XX-Large
        ///     *  X-Small and 3X-Large crowd in if they are available.
        ///     
        /// Sample request:
        ///
        ///     GET /Clothing/2001
        ///
        /// </remarks>
        /// <returns>All the information about a clothing item</returns>
        /// <response code="200">Returns the item</response>
        /// <response code="400">If the item is null</response> 
        /// <response code="500">If there is a database error</response>    
        [HttpGet]
        [Route("api/clothing/{id}")]
        [ResponseType(typeof(Clothing))]
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

        /// <summary>
        /// Gets a filtered list of clothing
        /// </summary>
        /// <param name="filter">
        ///   +  Catagory
        ///   +  Size
        ///   +  SleeveLength 
        /// </param>
        /// <param name="id">Ids depend on the filter</param>
        /// <remarks>
        /// Clothing Ids are coded into broad catagories:
        ///  *  Catagory
        ///     +  AdultTs
        ///     +  Cams _(Camisoles)_
        ///     +  ChildsTs
        ///     +  BabyTs
        ///     +  Onesies
        ///  *  Size 
        ///     +  Onesies
        ///         +  3-6m
        ///         +  6-9m
        ///         +  9-12m
        ///         +  12-18m
        ///     +  Children's Shirts
        ///         +  y8-10
        ///         +  y-sm
        ///         +  y-med
        ///         +  y-lg
        ///         +  y-xlg
        ///     +  Adult Shirts
        ///         +  ad-sm
        ///         +  ad-med
        ///         +  ad-lg
        ///         +  ad-xlg
        ///         +  ad-xxl
        ///  *  Sleeve Length
        ///     +  no *(Tank tops, Camisoles)*
        ///     +  sh *(short sleeves)*
        ///     +  fl *(long sleeves)*
        ///     +  34 *(3/4 length)*
        ///     
        /// Sample request:
        ///
        ///     GET /Clothing/catagory/onesies
        ///     GET /Clothing/size/ad-med
        ///     GET /Clothing/sleevelength/no
        ///
        /// </remarks>
        /// <returns>A filtered list of Clothing items</returns>
        /// <response code="200">Returns the filtered list</response>
        /// <response code="400">If the item is null</response> 
        /// <response code="500">If there is a database error</response>    
        [HttpGet]
        [Route("api/clothing/{filter}/{id}")]
        [ResponseType(typeof(Clothing))]
        public IHttpActionResult GetClothingByFilter(string filter, string id)
        {
            try
            {
                var clothing = new List<Clothing>();
                filter = filter.ToLower();
                switch (filter)
                {
                    case "catagory" :
                        clothing = _repository.Clothing.FindByCondition(c => c.Catagory == id).ToList();
                        break;
                    case "size":
                        clothing = _repository.Clothing.FindByCondition(c => c.Size == id).ToList();
                        break;
                    case "sleevelength":
                        clothing = _repository.Clothing.FindByCondition(c => c.SleeveLength == id).ToList();
                        break;
                    default:
                        break;
                }
                if (object.ReferenceEquals(clothing, null))
                {
                    _logger.LogError($"Clothing with id: {filter}, hasn't been found in db.");
                    return BadRequest($"Clothing with id: {filter}, hasn't been found in db.");
                }
                _logger.LogInfo($"{MethodBase.GetCurrentMethod().Name} returned {clothing} in {GetType().Name} from the database");
                return Ok(clothing);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the {MethodBase.GetCurrentMethod().Name} action: {ex.Message}");
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// [Admin] Updates a Clothing item in the product base  
        /// Mainly used for updating inventory (quantity)
        /// </summary>
        /// <param name="id">
        /// The ID of the item you want to update
        /// </param>
        /// <param name="item">
        /// Clothing item with same id, update the rest of the data.  
        /// </param>
        /// <remarks>
        /// 
        ///   Sample request:
        ///
        /// PUT /Clothing/2001
        /// {
        ///     "prodId": "2001",
        ///     "prodPicture": "/images/2001.png",
        ///     "prodPrice": 39.95,
        ///     "prodQty": 10, _(update Qty to correct value)_
        ///     "catagory": "childsts",  _(GET /Catagories)_
        ///     "backPicture": "/images/2002.png",  
        ///     "size": "y-sm",  _(GET /Size)_
        ///     "sleeveLength": "sh",  _(GET /Sleeves)_
        /// }           
        ///
        /// </remarks>
        /// <returns>All the information about the Clothing item</returns>
        /// <response code="204">Sucessful update, NoContent</response>
        /// <response code="400">If the item is null or invalid</response> 
        /// <response code="500">If there is a database error</response> 
        [HttpPut]
        [Authorize]
        [Route("api/clothing/{id}")]
        public IHttpActionResult UpdateClothing(string id, [FromBody]Clothing item)
        {
            try
            {
                if (item == null)
                {
                    _logger.LogError($"{MethodBase.GetCurrentMethod().Name} Clothing sent from client is null.");
                    return BadRequest("Clothing is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError($"{MethodBase.GetCurrentMethod().Name} Clothing model is invalid {item}");
                    return BadRequest($"Invalid model {item}");
                }

                Clothing dbClothing = _repository.Clothing.GetClothingById(id);
                if (dbClothing == null)
                {
                    _logger.LogError($"{MethodBase.GetCurrentMethod().Name} Clothing with id:{id} couldn't be found");
                    return NotFound();
                }

                _repository.Clothing.UpdateClothing(dbClothing, item);
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
        /// The ID of the item you want to delete
        /// </param>
        /// <param name="item">
        ///     The item to be deleted
        /// </param>
        /// <remarks>
        /// Only removes the item from the database for now  
        /// Remeber to remove or overwrite the pictures  
        /// Sample request:
        ///
        ///     DELETE /Clothing/{id}
        ///
        /// </remarks>
        /// <response code="200">If sucessful</response>
        /// <response code="400">If the item is null or invalid</response> 
        /// <response code="500">If there is a database error</response> 
        [HttpDelete]
        [Authorize]
        [Route("api/Clothing/{id}")]
        public IHttpActionResult DeleteClothing(string id, [FromBody] Clothing item)
        {
            try
            {
                if (item == null)
                {
                    _logger.LogError($"{MethodBase.GetCurrentMethod().Name} Clothing sent from client is null.");
                    return BadRequest("Clothing is null");
                }

                if (id != item.ProdId)
                {
                    _logger.LogError($"{MethodBase.GetCurrentMethod().Name} Clothing sent from client doesn't match id.");
                    return BadRequest($"Clothing id: {id} doesn't match item: {item}");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError($"{MethodBase.GetCurrentMethod().Name} Clothing model is invalid {item}");
                    return BadRequest($"Invalid model {item}");
                }

                Clothing dbClothing = _repository.Clothing.GetClothingById(id);
                if (dbClothing == null)
                {
                    _logger.LogError($"{MethodBase.GetCurrentMethod().Name} Clothing with id:{item.ProdId} couldn't be found");
                    return NotFound();
                }

                _repository.Clothing.DeleteClothing(item);
                _repository.Save();
                return Ok($"Removed {dbClothing} from inventory");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
