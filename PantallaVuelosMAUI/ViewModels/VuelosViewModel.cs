using PantallaVuelosMAUI.Models;
using PantallaVuelosMAUI.Repositories;
using PantallaVuelosMAUI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PantallaVuelosMAUI.ViewModels
{
    public class VuelosViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Vuelo> Vuelos { get; set; }
        public Vuelo Vuelo { get; set; }

        VuelosRepository<VueloBuffer> bufferRepository = new();

        //Objetos
        VueloService service;

        public VuelosViewModel()
        {
            service = new();
            Llenar();
        }

        private async void Llenar()
        {
            //Recupero los registros de la bd local (provienen de la api)
            var lista = (await service.GetAsync()).ToList();

            //Abrir el buffer
            var pendientes = bufferRepository.GetAll();

            foreach (var item in pendientes)
            {
                if (item.Status == State.Agregado)
                {
                    lista.Add(new Vuelo
                    {
                        Id=0,
                        Destino=item.Destino,
                        Estado=item.Estado,
                        Fecha=item.Fecha,
                        Numerovuelo=item.Numerovuelo,
                        Puerta=item.Puerta,
                    });
                }

                if(item.Status==State.Modificafo)
                {
                    var anterior = lista.FirstOrDefault(x => x.Numerovuelo == item.Numerovuelo);
                    anterior.Destino = item.Destino;
                    anterior.Puerta = item.Puerta;
                    anterior.Estado = item.Estado;
                }


            }

            Vuelos.Clear();
            lista.ForEach(x=>Vuelos.Add(x));
        }

        public void Actualizar(string nombre = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));    
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
