using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace codxFrank.ABTesting.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger _logger;

        public ValuesController(ILogger<ValuesController> logger)
        {
            _logger = logger;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            _logger.LogInformation("_logger: LogInformation");
            _logger.LogWarning("_logger: LogWarning");
            _logger.LogError("_logger: LogError");
            _logger.LogCritical("_logger: LogCritical");
            //Log.Debug("You should click the clap button if you found this post useful!");
            return new[] { "value1", "value2" };
        }
    }
}
