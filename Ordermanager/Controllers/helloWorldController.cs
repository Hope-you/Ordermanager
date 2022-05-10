using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Ordermanager.Bll;
using Ordermanager.Model;

namespace Ordermanager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class helloWorldController : ControllerBase
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
            return "Hello World!";
        }


    }
}
