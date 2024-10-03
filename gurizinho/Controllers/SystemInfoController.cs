using gurizinho.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gurizinho.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SystemInfoController : ControllerBase
    {

        private readonly IConfiguration _appProprerties;
        private readonly ILogger<SystemInfoController> _logger;

        public SystemInfoController(IConfiguration appProprerties, ILogger<SystemInfoController> logger)
        {
            _appProprerties = appProprerties;
            _logger = logger;
        }

        [HttpGet("/info/database")]
        public IActionResult GetInfo() {


            //teste do exeption handler global
            throw new NotImplementedException();

            _logger.LogInformation("printado info do banco de dados");

            return Ok(_appProprerties.GetConnectionString("Postgres"));
        }
    }
}
