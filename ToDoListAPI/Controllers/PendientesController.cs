using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoListAPI.Models;
using ToDoListAPI.Repository;

namespace ToDoListAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PendientesController : ControllerBase
    {
        Repository<Actividade> Repository;

        public PendientesController(Sistem21TodolistContext context)
        {
            Repository = new Repository<Actividade>(context);
        }

        public IActionResult Get()
        {
            var lista = Repository.GetAll();

            return Ok(lista);
        }

        [HttpPost]
        public IActionResult Post(Actividade actividad)
        {
            if (string.IsNullOrWhiteSpace(actividad.Descripcion))
            {
                return BadRequest("La descripción no debe estar vacío. Escriba una para continuar");
            }

            actividad.Id = 0;
            Repository.Insert(actividad);

            return Ok();
        }

        [HttpPut]
        public IActionResult Put(Actividade actividad)
        {
            if (string.IsNullOrWhiteSpace(actividad.Descripcion))
            {
                return BadRequest("La descripción no debe estar vacío. Escriba una para continuar");
            }

            var act = Repository.GetByID(actividad.Id);

            if (act != null)
            {
                act.Descripcion = actividad.Descripcion;
                Repository.Update(act);
                return Ok();
            }
            else
                return NotFound();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var act = Repository.GetByID(id);

            if (act != null)
            {
                Repository.Delete(act);
                return Ok();
            }
            else
                return NotFound();

        }
    }
}
