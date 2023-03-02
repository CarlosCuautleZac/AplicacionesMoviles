using PantallaVuelosMAUI.Views;

namespace PantallaVuelosMAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainView();
        }
    }
}