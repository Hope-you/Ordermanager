using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Ordermanager.Bll;
using Ordermanager.Model;
using Microsoft.AspNetCore.Authorization;
using Ordermanager.Api.Controllers.BaseController;

namespace Ordermanager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class helloWorldController : PackageControllerBase
    {
        private readonly ILogger _logger;

        public helloWorldController(ILogger<helloWorldController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public string helloWorld()
        {
            _logger.Log(LogLevel.Debug, "helloWorld()", _logger);
            return _userId;
        }


    }
}
