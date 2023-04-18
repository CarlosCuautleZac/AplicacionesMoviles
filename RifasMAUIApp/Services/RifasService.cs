﻿using Newtonsoft.Json;
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

        public async void Post(BoletoDTO boleto)
        {
            //Validar
            //Se supone que se tiene que validar en todo, nosotros menjamos el modelo de 3 capas
            //Interfaz
            //Logica de negocios
            //Modelo de Datos

           await Send("api/rifas/pagar", boleto, HttpMethod.Post);
        }

        async Task Send(string url, object dto, HttpMethod method)
        {
            var json = JsonConvert.SerializeObject(dto);
            HttpRequestMessage httpRequestMessagerequest = new HttpRequestMessage(method, url);
            httpRequestMessagerequest.Content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.SendAsync(httpRequestMessagerequest);
            response.EnsureSuccessStatusCode();
        }

        public async void Put(BoletoDTO boleto)
        {
            await Send("api/rifas/vender", boleto, HttpMethod.Post);
        }

        //Se tiene que cancelar
        public async void Delete(BoletoDTO boleto)
        {
            await Send("api/rifas/cancelar", boleto, HttpMethod.Put);
        }
    }
}
