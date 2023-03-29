using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RifasAPI.Services;

namespace RifasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RifaController : ControllerBase
    {
        public RifaController(BoletosHub hub)
        {
            
        }
    }
}
