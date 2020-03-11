using System;
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



        private async void btnpagos_Clicked(object sender, EventArgs e)
        {
            //((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.Coral;
            await ((MainPage)App.Current.MainPage).Detail.Navigation.PushAsync(new Pagos());
        }
        

        private void BtnCerrarSesion_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new Login());
        }
    }
}