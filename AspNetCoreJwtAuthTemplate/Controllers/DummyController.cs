using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreJwtAuthTemplate.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class DummyController : ControllerBase
    {
        private static readonly string[] DumpData = new []
        {
            "Istanbul", "Ankara", "Izmir"
        };

        [HttpGet]
        public string[] Get(){
            return DumpData;
        }

    }
}