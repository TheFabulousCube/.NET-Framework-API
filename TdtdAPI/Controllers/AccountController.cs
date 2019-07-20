﻿using Entities.Models;
using Microsoft.AspNet.Identity;
using Repository;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace TdtdAPI.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private AuthRepository _repo = null;

        public AccountController()
        {
            _repo = new AuthRepository();
        }

        // POST api/Account/Register
        /// <summary>
        /// [Restricted] Adds a new Authorized account to the API
        /// </summary>
        /// <param name="userModel">
        /// 
        /// {
        ///     "userName": "userName",
        ///     "password": "password",
        ///     "confirmPassword": "password
        /// }           
        /// 
        /// </param>
        /// <remarks>This call registers a user for the API.
        /// This is different from the Users account.  
        ///   Sample request:
        ///
        /// POST /Account/Register
        /// {
        ///     "userName": "userName",
        ///     "password": "password",
        ///     "confirmPassword": "password
        /// }               
        ///
        /// </remarks>
        /// <returns>All the information about the magnet</returns>
        /// <response code="201">Returns the new authorized API account</response>
        /// <response code="400">If the item is null,invalid, or userName is already taken</response> 
        /// <response code="500">If there is a database error</response> 
        [Authorize]
        [Route("Register")]
        [ResponseType(typeof(UserModel))]
        public async Task<IHttpActionResult> Register(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await _repo.RegisterUser(userModel);

            IHttpActionResult errorResult = GetErrorResult(result);

            return errorResult ?? Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }

            base.Dispose(disposing);
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
