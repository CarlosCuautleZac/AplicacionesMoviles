using Newtonsoft.Json;
using PantallaVuelosMAUI.Models;
using PantallaVuelosMAUI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Windows.Media.Protection.PlayReady;

namespace PantallaVuelosMAUI.Services
{
    public class VueloService
    {
        HttpClient client;
        VuelosRepository repository = new VuelosRepository();

        //Sincronizador de microservicios
        public VueloService()
        {
            client.BaseAddress = new Uri("https://aerolineatec.sistemas19.com/");
        }

        public async Task<IEnumerable<Vuelo>> GetAsync()
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                //Descargar de la api
                var response = await client.GetAsync("api/vuelos");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                var vuelos = JsonConvert.DeserializeObject<List<Vuelo>>(json);
                

                //Actualizacion en la BD local
                foreach (var item in vuelos)
                {
                    var vuelo = repository.Get(item.Id);
                    if (vuelo == null)
                    {
                        repository.Insert(item);
                    }
                    else
                        repository.Update(item);
                }

                foreach (var item in repository.GetAll())
                {
                    if (!vuelos.Any(x => x.Id == item.Id))
                    {
                        repository.Delete(item.Id);
                    }
                }
            }

            return repository.GetAll();

        }
    }


}
