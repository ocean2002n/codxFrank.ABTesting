using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace codxFrank.ABTesting.Services.Controllers.v1
{
    [ApiVersion("1.0")]
    //[Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger _logger;

        public ValuesController(ILogger<ValuesController> logger)
        {
            _logger = logger;
        }

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

        [HttpGet]
        public async Task<IActionResult> Get2()
        {
            return Ok(new[] { "value1", "value2" });
        }

    }
}
