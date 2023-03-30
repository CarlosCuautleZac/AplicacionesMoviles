using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RifasAPI.Models;
using RifasAPI.Repostitories;
using RifasAPI.Services;
using System.Net;

namespace RifasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RifaController : ControllerBase
    {
        private readonly Sistem21RifasContext context;
        private readonly BoletosHub hubContext;
        BoletosRepository boletosRepository;

        public RifaController(Sistem21RifasContext context, BoletosHub hubContext)
        {
            this.context = context;
            this.hubContext = hubContext;
            boletosRepository = new BoletosRepository(context);
        }

        [HttpGet("vendidos")]
        public IActionResult Get()
        {
            return Ok(boletosRepository.GetAll());
        }

        //Modo fuera de linea
        [HttpGet("actualizar/{hora:DateTime}")]
        public IActionResult Get(DateTime hora)
        {
            return Ok(boletosRepository.GetAllByFecha(hora));
        }

        [HttpPost("vender")]
        public async Task<IActionResult> Post(Boletos boleto)
        {
            if(boletosRepository.Validate(boleto, out List<string> errores))
            {
                boleto.Id= 0;
                boleto.FechaModificacion = DateTime.Now.ToMexicoTime();
                boleto.Eliminado = 0;
                boletosRepository.Insert(boleto);

                await hubContext.Clients.All.SendAsync("agregar", boleto);

                return Ok();
            }
            else
            {
                return BadRequest(errores);
            }
        }

        [HttpPut("vender")]
        public async Task<IActionResult> Put(Boletos boleto)
        {
            var registro = boletosRepository.GetById(boleto.Id);

            if (registro == null || registro.Eliminado ==0)
                return NotFound("No existe el boleto especificado");

            else if (registro.Pagado == 1)
            {
                return Conflict("El boleto ya se encuentra pagado");
            }

            else
            {
                registro.Pagado = 1;
                registro.FechaModificacion = DateTime.Now.ToMexicoTime();
                boletosRepository.Update(registro);
                await hubContext.Clients.All.SendAsync("pagar", registro);
            }

            return Ok();
            
        }

        [HttpPut("cancelar")]
        public async Task<IActionResult> Cancelar(Boletos boleto)
        {
            var registro = boletosRepository.GetById(boleto.Id);

            if (registro == null || registro.Eliminado == 0)
                return NotFound("No existe el boleto especificado");

            else if (registro.Pagado == 1)
            {
                return Conflict("No se puede cancelar el boleto pagado");
            }

            else
            {
                registro.Eliminado = 1;
                registro.FechaModificacion = DateTime.Now.ToMexicoTime();
                boletosRepository.Update(registro);
                await hubContext.Clients.All.SendAsync("cancelar", registro);
            }

            return Ok();
        }
    }
}
