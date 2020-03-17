﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash2.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ImageCircle.Forms.Plugin;
using Wash2.Views.Pagos;
using Wash2.Views.Login;
using Wash2.Views.AutoLavados;
using Wash2.Views.Estado;
using Wash2.Views.Perfil;

namespace Wash2.Menu
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Master : ContentPage
	{

		public Master ()
		{

			InitializeComponent ();
            
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.Coral;
        }

        private async void Perfil_Tapped(object sender, EventArgs args)
        {
            App.MasterD.IsPresented = false;
            await ((MainPage)App.Current.MainPage).Detail.Navigation.PushAsync(new Perfil());
        }

        private async void Btnedoserv_Clicked(object sender, EventArgs e)
        {
            App.MasterD.IsPresented = false;
            await((MainPage)App.Current.MainPage).Detail.Navigation.PushAsync(new EdoServicio());
        }

        private async void btnpagos_Clicked(object sender, EventArgs e)
        {
            //((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.Coral;
            App.MasterD.IsPresented = false;
            await ((MainPage)App.Current.MainPage).Detail.Navigation.PushAsync(new Pagos());
        }

        private async void Btnautos_Clicked(object sender, EventArgs e)
        {
            App.MasterD.IsPresented = false;
            await ((MainPage)App.Current.MainPage).Detail.Navigation.PushAsync(new AutosLavados());
        }

        private void BtnCerrarSesion_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new Login());
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }
    }
}