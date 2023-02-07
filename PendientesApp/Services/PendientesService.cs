using Newtonsoft.Json;
using PendientesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PendientesApp.Services
{
    public class PendientesService
    {
        HttpClient client;
        public PendientesService()
        {
            client.BaseAddress = new Uri("https://pendientes.sistemas19.com/");
        }

        public async Task<List<Actividad>> GetAll()
        {
            List<Actividad> Actividades = new();

            var result = await client.GetAsync("api/pendientes");
            if(result.IsSuccessStatusCode)
            {
                var json = await result.Content.ReadAsStringAsync();
                Actividades = JsonConvert.DeserializeObject<List<Actividad>>(json);
            }
            return Actividades;
        }
    }
}
