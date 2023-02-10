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

        public ICommand SeleccionarCommand { get; set; }

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
            SeleccionarCommand = new Command<Actividad>(Seleccionar);

            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            pendientesService = new();
            _ = CargarDatos();
        }

        private async void Seleccionar(Actividad actividad)
        {
#if ANDROID
            if (actividadView == null)
                actividadView = new() { BindingContext = this };
#else
            actividadView = new() { BindingContext = this };
#endif

            Error = "";
            Actividad = actividad;
            Actualizar(nameof(Actividad));
            await Application.Current.MainPage.Navigation.PushAsync(actividadView);
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
                    if (Actividad.Id == 0)
                    {
                        await pendientesService.Insert(Actividad);
                    }
                    else
                    {
                        await pendientesService.Update(Actividad);
                    }
                    _ = CargarDatos();
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                Actualizar();
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                Actualizar();
            }

        }

        private async void Nuevo()
        {
            Actividad = new();
            Error = "";

#if ANDROID
            if (actividadView == null)
                actividadView = new() { BindingContext = this };
#else
            actividadView = new() { BindingContext = this };
#endif


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
