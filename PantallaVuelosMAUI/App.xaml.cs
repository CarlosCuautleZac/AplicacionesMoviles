﻿using PantallaVuelosMAUI.ViewModels;
using PantallaVuelosMAUI.Views;

namespace PantallaVuelosMAUI
{
    public partial class App : Application
    {


        public App()
        {
            InitializeComponent();

            Routing.RegisterRoute("estado", typeof(Views.EstadosView));


            MainPage = new AppShell();
        }
    }
}