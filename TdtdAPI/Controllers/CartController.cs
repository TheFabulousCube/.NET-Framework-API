using Contracts;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace TdtdAPI.Controllers
{
    [Route("api/carts")]
    public class CartsController : ApiController
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public CartsController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
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

        [HttpGet]
        [Route("api/carts/{userId}/{prodID}")]
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

        [HttpGet]
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
    }
}
