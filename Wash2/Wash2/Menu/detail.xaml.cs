using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.Coral;
        }
        

        private async void Solicitudes_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("ErroR", "error status 500 InternalServerError", "ok");
        }
    }
}