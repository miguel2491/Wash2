using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash2.Views.Solicitudes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wash2.Menu
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
	public partial class detail : ContentPage
	{
		public detail ()
		{

            InitializeComponent ();
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Aviso", "Realmente quieres salir?", "Si", "No");
                if (result) System.Environment.Exit(0);

            });
            return true;
        }

        private async void Solicitudes_Clicked(object sender, EventArgs e)
        {
            await ((MainPage)App.Current.MainPage).Detail.Navigation.PushAsync(new SolicitaSolicitud());
        }
    }
}