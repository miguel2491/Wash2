using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wash2.Views.Solicitudes
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SolicitaSolicitud : ContentPage
	{
		public SolicitaSolicitud ()
		{
			InitializeComponent ();
		}

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Solicitud", "Aceptar Solicitud", "Si", "No");
            Debug.WriteLine("Answer: " + answer);
            if (answer == true) {
                await DisplayAlert("", "Aqui va algo", "ok");
            }
        }

        private async void Btn_CheckIn_Clicked(object sender, EventArgs e)
        {
            
            //await Navigation.PushAsync(new EstadoServicio());
        }
    }
}