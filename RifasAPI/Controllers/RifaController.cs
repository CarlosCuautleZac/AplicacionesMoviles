using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RifasAPI.Models;
using RifasAPI.Repostitories;
using RifasAPI.Services;

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

        public IActionResult Get()
        {
            return Ok(boletosRepository.GetAll());
        }
    }
}
