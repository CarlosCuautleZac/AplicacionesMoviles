using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PruebaSignalR.Services;

namespace PruebaSignalR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Numeros : ControllerBase
    {

        private readonly IHubContext<NumerosHub> numerosHub;

        public Numeros(IHubContext<NumerosHub> context)
        {
            this.numerosHub = context;
        }

        [HttpGet("Incrementar")]
        public IActionResult Incrementar()
        {
            numerosHub.Clients.All.SendAsync("Incrementar");
            return Ok();
        }

        [HttpGet("Decrementar")]
        public IActionResult Decrementar()
        {
            numerosHub.Clients.All.SendAsync("Decrementar");
            return Ok();
        }
    }
}
