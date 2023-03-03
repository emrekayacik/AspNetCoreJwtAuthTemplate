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
        public async Task<IActionResult> GetAllAsync(){
            return DumpData.Count() > 0 ? Ok(DumpData) : NotFound();
        }

    }
}