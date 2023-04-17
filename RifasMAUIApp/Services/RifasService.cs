using Newtonsoft.Json;
using RifasMAUIApp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RifasMAUIApp.Services
{
    public class RifasService
    {
        HttpClient client;

        public RifasService()
        {
            client.BaseAddress = new Uri("https://rifas.sistemas19.com/");
        }

        public async Task<IEnumerable<BoletoDTO>> GetAll()
        {
            var response = await client.GetAsync("api/rifas/vendidos");
            response.EnsureSuccessStatusCode();//Verificar que regreso un 200

            var json = await response.Content.ReadAsStringAsync();
            var datos = JsonConvert.DeserializeObject<IEnumerable<BoletoDTO>>(json);
            return datos;

        }

        public void Post()
        {

        }

        public void Put()
        {

        }

        //Se tiene que cancelar
        public void Delete()
        {

        }
    }
}
