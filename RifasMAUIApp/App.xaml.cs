using RifasMAUIApp.ViewModels;
using RifasMAUIApp.Views;

namespace RifasMAUIApp
{
    public partial class App : Application
    {
        public static RifaViewModel ViewModel { get; set; } = new();
        public App()
        {
            InitializeComponent();

            Routing.RegisterRoute("//Agregar", typeof(VenderBoletoView));

            MainPage = new AppShell();
        }
    }
}