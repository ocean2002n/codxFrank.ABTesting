using codxFrank.ABTesting.Services.Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace codxFrank.ABTesting.Services.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
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

            throw new Exception("Exception while fetching all the students from the storage.");
            
            return new[] { "value1", "value2" };
        }

        [HttpGet]
        public async Task<IActionResult> Get2()
        {
            return Ok(new[] { "value2", "value3" });
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginViewModel login)
        {
            return Ok("Hi my name is Frank");
        }

    }
}
