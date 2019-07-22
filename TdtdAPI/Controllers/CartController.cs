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
    /// <summary>
    /// This is a summary on the class (more useful)
    /// </summary>
    [Route("api/carts")]
    public class CartsController : ApiController
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        /// <summary>
        ///  This is a summary on the controler
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="repository"></param>
        public CartsController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// Gets a list of all the rows in the cart table
        /// </summary>
        /// <remarks>
        /// ### REMARKS ###
        /// - Each row represents the Qty of a specific item (ProdId) for a specific User
        /// - Not generally useful as a public API
        /// - Only needed for accounting?
        /// </remarks>
        /// <returns>An array of all sleeve lengths</returns>
        /// <response code="200">Returns the list of available sleeve lengths</response>
        /// <response code="500">If there is a database error</response> 
        [HttpGet]
        [ResponseType(typeof(List<Carts>))]
        public IHttpActionResult GetAllCarts()
        {
            try
            {
                var carts = _repository.Cart.GetAllCarts();
                _logger.LogInfo($"{MethodBase.GetCurrentMethod().Name} returned {carts.Count()} carts in {GetType().Name} from the database");
                return Ok(carts);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the GetAllCarts action: {ex.Message}");
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Gets a specific line item from a cart
        /// </summary>
        /// <param name="userId">Id of the user this cart is assigned to</param>
        /// <param name="prodId">Product id of the item</param>
        /// <remarks>
        /// This would be useful as a PUT to update the Qty.
        ///
        /// </remarks>
        /// <returns>The qty of a line item for a specific user</returns>
        /// <response code="200">Returns the qty of a line item for a specific user</response>
        /// <response code="400">If the item is null</response> 
        /// <response code="500">If there is a database error</response>    
        [HttpGet]
        [Route("api/carts/{userId}/{prodID}", Name = "CartById")]
        [ResponseType(typeof(Carts))]
        public IHttpActionResult GetCartById(int userId, string prodId)
        {
            try
            {
                var cart = _repository.Cart.GetCartById(userId, prodId);
                if (object.ReferenceEquals(cart, null))
                {
                    _logger.LogError($"Couldn't find cart for user:{userId} containing product: {prodId}");
                    return BadRequest($"Couldn't find cart for user:{userId} containing product: {prodId}");
                }
                return Ok(cart);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the {MethodBase.GetCurrentMethod().Name} action: {ex.Message}");
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Gets a shopping cart for a specific user
        /// </summary>
        /// <param name="id">Id of the user for this cart</param>
        /// <remarks>
        /// ### REMARKS ###
        /// - Needed to display a cart
        /// - Needed for checkout
        /// </remarks>
        /// <returns>The qty of a line item for a specific user</returns>
        /// <response code="200">Returns an array of products and the qty of each in their cart</response>
        /// <response code="400">If the item is null</response> 
        /// <response code="500">If there is a database error</response>    
        [HttpGet]
        [ResponseType(typeof(List<Carts>))]
        [Route("api/carts/{id}")]
        public IHttpActionResult GetCartByUser(int id)
        {
            try
            {
                var cart = _repository.Cart.GetCartsByUser(id);

                if (object.ReferenceEquals(cart, null))
                {
                    _logger.LogError($"Cart with id: {id}, hasn't been found in db.");
                    return BadRequest($"Cart with id: {id}, hasn't been found in db.");
                }
                _logger.LogInfo($"{MethodBase.GetCurrentMethod().Name} returned {cart.Count()} items in {GetType().Name} from the database");
                return Ok(cart);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the {MethodBase.GetCurrentMethod().Name} action: {ex.Message}");
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// [Restricted] AddToCart (Upsert)
        /// </summary>
        /// <param name="cart">
        /// From Body:        
        ///  {
        ///     "userId": int,
        ///     "prodID": {string for Magnets, int for Clothing},
        ///     "qty": 1
        ///  } 
        /// </param>
        /// <remarks>
        ///   Quantities less than or equal 0 will remove the item from the cart  
        ///   Quantities greater than available will be truncated
        ///   (You can check available quantity by calling **GetMagnetById(string id)**)
        ///   
        ///   Sample request:
        ///
        /// POST /Carts
        /// {
        ///     "userId": 2,
        ///     "prodID": "2003",
        ///     "qty": 1
        /// }
        ///
        /// </remarks>
        /// <returns>The same cart with updated quantity</returns>
        /// <response code="201">Returns the cart with updated qty</response>
        /// <response code="400">If the item is null or invalid</response> 
        /// <response code="500">If there is a database error</response> 
        [HttpPut]
        [Authorize]
        [ResponseType(typeof(Carts))]
        public IHttpActionResult AddToCart([FromBody]Carts cart)
        {
            try
            {
                if (cart == null)
                {
                    _logger.LogError($"{MethodBase.GetCurrentMethod().Name} Cart object sent from client is null.");
                    return BadRequest("Cart object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError($"{MethodBase.GetCurrentMethod().Name} Invalid Cart object sent from client.");
                    return BadRequest($"Invalid model {cart}");
                }

                _repository.Cart.AddToCart(cart);
                _repository.Save();
                return CreatedAtRoute<Carts>("CartById", new { userId = cart.UserId, prodID = cart.ProdID }, cart);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// [Restricted] EmptyCart (By user Id)
        /// </summary>
        /// <param name="id">
        ///     "id": int
        /// </param>
        /// <remarks>
        /// Completely removes ALL items from a user's cart
        /// Sample request:
        ///
        ///     DELETE /Carts/{userId}
        ///
        /// </remarks>
        /// <response code="201"></response>
        /// <response code="400">If the item is null or invalid</response> 
        /// <response code="500">If there is a database error</response> 
        [HttpDelete]
        [Authorize]
        [Route("api/carts/{id}")]
        public IHttpActionResult EmptyCart(int id)
        {
            try
            {
                _repository.Cart.EmptyCart(id);
                _repository.Save();
                return Ok($"Removed all items from user {id}'s cart");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
