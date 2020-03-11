using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wash2.Views.Pagos
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Pagos : ContentPage
	{
		public Pagos ()
		{
			InitializeComponent ();
		}
        

        private async void BtnInfo_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("INFORMACIÓN", "Ganas el 25% de cada lavada Cada viernes recibiras tu dinero registra tu tarjeta", "ACEPTAR");
        }
    }
}