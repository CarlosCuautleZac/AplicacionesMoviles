﻿using Newtonsoft.Json;
using PantallaVuelosMAUI.Models;
using PantallaVuelosMAUI.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
//using Windows.Media.Protection.PlayReady;

namespace PantallaVuelosMAUI.Services
{
    public class VueloService
    {
        public event Action<List<Vuelo>> VuelosActualizados;

        HttpClient client = new();
        VuelosRepository<Vuelo> repository = new();
        VuelosRepository<VueloBuffer> bufferRepository = new();
        Thread Sincronizador;

        public string Errores { get; private set; }

        //Sincronizador de microservicios
        public VueloService()
        {
            client.BaseAddress = new Uri("https://aerolineatec.sistemas19.com/");
            Sincronizador = new Thread(new ThreadStart(Sincronizar));
            Sincronizador.IsBackground = true;
            Sincronizador.Start();
        }

        private async void Sincronizar()
        {

            while (true)
            {
                Thread.Sleep(30000); //Dormirlo 
                if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                {
                    //Descargar de la api
                    var response = await client.GetAsync("api/vuelos");
                    response.EnsureSuccessStatusCode();
                    var json = await response.Content.ReadAsStringAsync();
                    var vuelos = JsonConvert.DeserializeObject<List<Vuelo>>(json);
                    bool actualizado = false;


                    //Actualizacion en la BD local
                    foreach (var item in vuelos)
                    {
                        var vuelo = repository.Get(item.Id);
                        if (vuelo == null)
                        {
                            repository.Insert(item);
                             actualizado = true;
                        }
                        else
                        {
                            repository.Update(item);
                            actualizado = true;
                        }
                    }

                    foreach (var item in repository.GetAll())
                    {
                        if (!vuelos.Any(x => x.Id == item.Id))
                        {
                            repository.Delete(item.Id);
                            actualizado = true;
                        }
                    }

                    if (actualizado)
                    {
                        VuelosActualizados?.Invoke(repository.GetAll().ToList());
                    }
                }
            }
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

            var v = repository.GetAll().ToList();

            return v;

        }

        public async Task<bool> Post(Vuelo v)
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                var json = JsonConvert.SerializeObject(v);
                var response = await client.PostAsync("api/vuelos", new StringContent(json,
                    Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                    return true;
                else
                {
                    Errores = await response.Content.ReadAsStringAsync();
                    return false;
                }
            }
            else
            {
                VueloBuffer vb = new VueloBuffer()
                {
                    Destino = v.Destino,
                    Estado = v.Estado,
                    Fecha = v.Fecha,
                    Numerovuelo = v.Numerovuelo,
                    Puerta = v.Puerta,
                    Status = State.Agregado
                };

                bufferRepository.Insert(vb);
                return true;
            }



        }

        public async Task<bool> PutaAsync(Vuelo v)
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                var json = JsonConvert.SerializeObject(v);
                var response = await client.PutAsync("api/vuelos/cambiarestado", new StringContent(json,
                    Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                    return true;
                else
                {
                    Errores = await response.Content.ReadAsStringAsync();
                    return false;
                }
            }
            else
            {
                VueloBuffer vb = new VueloBuffer()
                {
                    Destino = v.Destino,
                    Estado = v.Estado,
                    Fecha = v.Fecha,
                    Numerovuelo = v.Numerovuelo,
                    Puerta = v.Puerta,
                    Status = State.Modificafo
                };

                bufferRepository.Insert(vb);
                return true;
            }



        }

        //public async Task<bool> DeleteAsync(Vuelo v)
        //{
        //    if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
        //    {
        //        var response = await client.DeleteAsync("api/vuelos/"+v.Id);

        //        if (response.IsSuccessStatusCode)
        //            return true;
        //        else
        //        {
        //            Errores = await response.Content.ReadAsStringAsync();
        //            return false;
        //        }
        //    }
        //    else
        //    {

        //        VueloBuffer vb = new VueloBuffer()
        //        {
        //            Destino = v.Destino,
        //            Estado = v.Estado,
        //            Fecha = v.Fecha,
        //            Numerovuelo = v.Numerovuelo,
        //            Puerta = v.Puerta,
        //            Status = State.Modificafo
        //        };

        //        bufferRepository.Insert(vb);
        //        return false;
        //    }



        //}
    }


}
