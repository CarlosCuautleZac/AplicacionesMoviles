using PendientesApp.Models;
using PendientesApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PendientesApp.ViewModels
{
    public class PendientesViewModel : INotifyPropertyChanged
    {

        PendientesService pendientesService;

        public ObservableCollection<Actividad> Actividades { get; set; } = new();

        public string Error { get; set; }


        public PendientesViewModel()
        {
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            pendientesService = new();
            _ = CargarDatos();
        }

        private async void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            await CargarDatos();
        }

        async Task CargarDatos()
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet) //Verficar si hay conexión a internet
            {
                var lista = await pendientesService.GetAll();
                Actividades.Clear();
                lista.ForEach(a => Actividades.Add(a));
                Error = "";
            }
            else
            {
                Error = "No hay conexión internet";

            }

            Actualizar();
        }

        public void Actualizar(string nombre = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
