using RifasMAUIApp.Views;

namespace RifasMAUIApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Routing.RegisterRoute("//Agregar", typeof(VenderBoletoView));

            MainPage = new AppShell();
        }
    }
}