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
        public IHttpActionResult Getwithresp()
        {
            var magnets = _repoWrapper.Magnets.FindAll();

            //_logger.LogInfo("Here is info message from our values controller.");

            return Ok(magnets);
        }

        [HttpGet]
        public IHttpActionResult GetMagnetById(string id)
        {
            var magnet = _repoWrapper.Magnets.GetMagnetById(id);

            //_logger.LogInfo("Here is info message from our values controller.");

            return Ok(magnet);
        }
    }
}
