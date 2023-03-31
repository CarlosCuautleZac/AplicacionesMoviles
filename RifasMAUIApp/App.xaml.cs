using RifasMAUIApp.Views;

namespace RifasMAUIApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Main();
        }
    }
}