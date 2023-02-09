using PendientesApp.Models;
using PendientesApp.Services;
using PendientesApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PendientesApp.ViewModels
{
    public class PendientesViewModel : INotifyPropertyChanged
    {
        public ICommand NuevoCommand { get; set; }
        public ICommand GuardarCommand { get; set; }

        PendientesService pendientesService;

        public ObservableCollection<Actividad> Actividades { get; set; } = new();
        public Actividad Actividad { get; set; }

        public string Error { get; set; }


        ActividadView actividadView;

        //Constructor
        public PendientesViewModel()
        {
            NuevoCommand = new Command(Nuevo);
            GuardarCommand = new Command(Guardar);

            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            pendientesService = new();
            _ = CargarDatos();
        }

        private async void Guardar()
        {
            try
            {
                Error = "";
                if (string.IsNullOrWhiteSpace(Actividad.Descripcion))
                    Error = "Escriba una descripción para la actividad";

                else if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                {
                    Error = "No hay conexión a internet";
                }
                else
                {
                    await pendientesService.Insert(Actividad);
                    _= CargarDatos();
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                Actualizar();
            }
            catch(Exception ex)
            {
                Error = ex.Message;
                Actualizar();
            }

        }

        private async void Nuevo()
        {
            Actividad = new();
            Error = "";
            if (actividadView == null)
                actividadView = new() { BindingContext = this };

            await Application.Current.MainPage.Navigation.PushAsync(actividadView);
            Actualizar();
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
