using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TdtdAPI.Controllers
{
    public class TestController : ApiController
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repoWrapper;

        public TestController(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
 
        [HttpGet]
        public IEnumerable<SizeLookup> Getwithresp()
        {
            var carts = _repoWrapper.Cart.FindAll();
            var catagories = _repoWrapper.CatagoryLookup.FindAll();
            var magnets = _repoWrapper.Magnets.FindAll();
            var clothing = _repoWrapper.Clothing.FindAll();
            //var downloads = _repoWrapper.Downloads.FindAll();
            var roles = _repoWrapper.RoleLookup.FindAll();
            var sizes = _repoWrapper.SizeLookup.FindAll();
            var sleeves = _repoWrapper.SleeveLookup.FindAll();
            var users = _repoWrapper.Users.FindAll();

            //_logger.LogInfo("Here is info message from our values controller.");

            return (sizes);
        }

        //[HttpGet]
        //public IHttpActionResult GetMagnetById(string id)
        //{
        //    var magnet = _repoWrapper.Magnets.GetMagnetById(id);

        //    //_logger.LogInfo("Here is info message from our values controller.");

        //    return Ok(magnet);
        //}
    }
}
