using AerolineaTECAPI.Models;
using AerolineaTECAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AerolineaTECAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Vuelos : ControllerBase
    {
        Repository<Vuelo> repository;

        public Vuelos(Sistem21AerolineadbContext context)
        {      
            repository = new(context);
        }

        public IActionResult Get()
        {
            return Ok(repository.Get());
        }

        [HttpPost]
        public IActionResult Post(Vuelo vuelo)
        {
            if (vuelo == null)
            {
                return BadRequest("Proporcione los datos del vuelo");
            }

            if (Validar(vuelo, out List<string> errores))
            {
                return Ok();
            }
            else
                return BadRequest(errores);
            
        }

        private bool Validar(Vuelo vuelo, out List<string> errors)
        {
            errors = new List<string>();

            if (string.IsNullOrEmpty(vuelo.Destino))
            {
                errors.Add("El destino del vuelo no puede ir vacio");
            }
            if (string.IsNullOrWhiteSpace(vuelo.Numerovuelo))
            {
                errors.Add("El numero de vuelo no puede ir vacio");
            }
            if (repository.Get().Any(x => x.Numerovuelo == vuelo.Numerovuelo && x.Id != vuelo.Id))
            {
                errors.Add("Ya existe un vuelo con el mismo numero, agregar otro");
            }
            if (vuelo.Puerta != 0)
            {
                if (repository.Get().Any(x => x.Puerta == vuelo.Puerta && x.Id != vuelo.Id))
                {
                    errors.Add("Ya existe un vuelo con el mismo numero, agregar otro");
                }
            }
            if (vuelo.Fecha <= DateTime.Now)
            {
                errors.Add("La fecha y hora no puede ser menor e igual a la actual");
            }
            return errors.Count == 0;
        }
    }
}
