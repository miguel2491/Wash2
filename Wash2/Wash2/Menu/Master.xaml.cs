using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash2.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ImageCircle.Forms.Plugin;

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
        private async void btnconfiguracion_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("AQUI", "ALGO", "ok");
        }
        private async void btnlavos_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("AQUI", "ALGO", "ok");
        }
        
    }
}