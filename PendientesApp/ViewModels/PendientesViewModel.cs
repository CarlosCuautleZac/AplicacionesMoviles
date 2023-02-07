using PendientesApp.Models;
using PendientesApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PendientesApp.ViewModels
{
    public class PendientesViewModel : INotifyCollectionChanged
    {

        PendientesService pendientesService;

        public ObservableCollection<Actividad> Actividades { get; set; } = new();


        public PendientesViewModel()
        {
            pendientesService = new();
            CargarDatos();
        }

        private async void CargarDatos()
        {
            //Verificar si tengo internet
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                Actividades.Clear();

            }
        }

        public void Actualizar(string nombre)
        {

        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}
