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
        //GET api/test
        //[HttpGet]
        // public IEnumerable<Magnets> Get()
        // {
        //     var magnets = _repoWrapper.Magnet.FindAll();

        //     //_logger.LogInfo("Here is info QA message from our values controller.");
        //     //_logger.LogDebug("Here is debug QA message from our values controller.");
        //     //_logger.LogWarn("Here is warn QA message from our values controller.");
        //     //_logger.LogError("Here is error QA message from our values controller.");

        //     return magnets;
        // }
        [HttpGet]
        public IHttpActionResult Getwithresp()
        {
            var magnets = _repoWrapper.Magnets.FindAll();

            //_logger.LogInfo("Here is info QA message from our values controller.");
            //_logger.LogDebug("Here is debug QA message from our values controller.");
            //_logger.LogWarn("Here is warn QA message from our values controller.");
            //_logger.LogError("Here is error QA message from our values controller.");

            return Ok(magnets);
        }

        [HttpGet]
        public IHttpActionResult GetMagnetById(string id)
        {
            var magnet = _repoWrapper.Magnets.GetMagnetById(id);

            //_logger.LogInfo("Here is info QA message from our values controller.");
            //_logger.LogDebug("Here is debug QA message from our values controller.");
            //_logger.LogWarn("Here is warn QA message from our values controller.");
            //_logger.LogError("Here is error QA message from our values controller.");

            return Ok(magnet);
        }
    }
}
