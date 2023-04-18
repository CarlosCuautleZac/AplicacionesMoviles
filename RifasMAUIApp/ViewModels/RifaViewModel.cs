using RifasMAUIApp.Models;
using RifasMAUIApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RifasMAUIApp.ViewModels
{
    public class RifaViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<Boleto> Boletos { get; set; } = new();

        public Boleto Boleto { get; set; }

        public string Error { get; set; }

        //No lo usamos en la vista porque es para movil y tiene el backbutton
        //public ICommand CancelarCommand { get; set; }
        public ICommand PagarCommand { get; set; }
        public ICommand VenderCommand { get; set; }
        public ICommand NuevaVentaCommand { get; set; }
        public ICommand CancelarVentaCommand { get; set; }


        RifasService service;
        public RifaViewModel()
        {
            service = new();
            DescargarBoletos();
        }

        private async void DescargarBoletos()
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                var vendidos = await service.GetAll();

                if (Boletos.Count < 50)
                {
                    for (uint i = 1; i <= 50; i++)
                    {
                        Boleto b = new()
                        {
                            NumeroBoleto = i
                        };
                        Boletos.Add(b);
                    }
                }


                //Pruebas
                //foreach (var v in vendidos)
                //{
                //    var posicion = v.NumeroBoleto;

                //    Boleto BoletoLista = new()
                //    {
                //        Id = v.Id,
                //        NombrePersona = v.NombrePersona,
                //        NumeroBoleto = v.NumeroBoleto,
                //        NumeroTelefono = v.NumeroTelefono,
                //        Pagado = v.Pagado
                //    };

                //    Boletos[(int)posicion] = BoletoLista;
                //}


                Actualizar();
            }
            else
            {
                Error = "No hay conexión a internet";
            }

        }

        public void Actualizar(string nombre = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(nombre)));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
