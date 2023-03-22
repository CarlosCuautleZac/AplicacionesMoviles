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
using System.Windows.Input;

namespace PantallaVuelosMAUI.ViewModels
{
    public class VuelosViewModel : INotifyPropertyChanged
    {
        public ICommand AgregarCommand { get; set; }

        VuelosRepository<VueloBuffer> bufferRepository = new();
        public ObservableCollection<Vuelo> Vuelos { get; set; } = new();
        public Vuelo Vuelo { get; set; } = new();

        public string Error { get; set; } = "";




        //Objetos
        VueloService service;

        public VuelosViewModel()
        {
            AgregarCommand = new Command(Agregar);

            service = new();
            Llenar();

            service.VuelosActualizados += Service_VuelosActualizados;


        }

        private void Service_VuelosActualizados(List<Vuelo> nuevos)
        {


            MainThread.BeginInvokeOnMainThread(() =>
            {
              

                for (int i = 0; i < nuevos.Count; i++)
                {
                    var nuevo = nuevos[i];
                    var anterior = Vuelos.FirstOrDefault(x => x.Id == nuevo.Id);

                    if (anterior == null)
                    {
                        Vuelos.Add(nuevo);
                    }
                    else if (nuevo.UltimaEdicionFecha != anterior.UltimaEdicionFecha)
                    {
                        Vuelos[Vuelos.IndexOf(anterior)] = nuevo;
                    }

                    var paraborrar = Vuelos.Where(x => !nuevos.Any(y => x.Id == y.Id));

                    foreach (var item in paraborrar)
                    {
                        Vuelos.Remove(item);
                    }

                }


            });

        }

        private async void Agregar()
        {
            //Validar


            var seagrego = await service.Post(Vuelo);

            if (seagrego)
            {
                Llenar();
                Vuelos = new();
                await Shell.Current.GoToAsync("//principal");

            }
            else
            {
                Error = service.Errores;
                Actualizar();
            }

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
                        Id = 0,
                        Destino = item.Destino,
                        Estado = item.Estado,
                        Fecha = item.Fecha,
                        Numerovuelo = item.Numerovuelo,
                        Puerta = item.Puerta,
                    });
                }

                if (item.Status == State.Modificafo)
                {
                    var anterior = lista.FirstOrDefault(x => x.Numerovuelo == item.Numerovuelo);
                    anterior.Destino = item.Destino;
                    anterior.Puerta = item.Puerta;
                    anterior.Estado = item.Estado;
                }


            }

            Vuelos.Clear();
            lista.ForEach(x => Vuelos.Add(x));
        }

        public void Actualizar(string nombre = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
