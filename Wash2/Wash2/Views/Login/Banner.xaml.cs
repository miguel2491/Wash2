using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wash2.Views.Login
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Banner : ContentPage
	{
		public Banner ()
		{
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent ();
            
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            // Application.Current.MainPage = new SignUp();
            Application.Current.MainPage = new NavigationPage(new Login());
        }

    }
}