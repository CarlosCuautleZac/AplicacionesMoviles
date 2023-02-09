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
            client = new HttpClient();
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

        public async Task Insert(Actividad a)
        {
            var json = JsonConvert.SerializeObject(a);
            StringContent scontent = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await client.PostAsync("api/pendientes", scontent);
            if (!result.IsSuccessStatusCode)
            {
                json = await result.Content.ReadAsStringAsync();
                string s = JsonConvert.DeserializeObject<string>(json);
                throw new Exception($"Ha ocurrido un error: {result.StatusCode}\n{s}");
            }
        }
    }
}
