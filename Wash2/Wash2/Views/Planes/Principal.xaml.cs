using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash2.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Wash2.Views.Planes;
using System.Net.Http;
using Newtonsoft.Json;

namespace Wash2.Views.Planes
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Principal : ContentPage
	{
		public Principal ()
		{
			InitializeComponent ();
            _ = GetPaquetes();
        }

        public async Task GetPaquetes() {
            HttpClient client = new HttpClient();

            var url = "http://www.washdryapp.com/app/public/paquete/listado";
            try {
                var response = await client.GetAsync(url);
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.InternalServerError:
                        Console.WriteLine("----------------------------------------------_____:Here status 500");
                        break;
                    case System.Net.HttpStatusCode.OK:
                        Console.WriteLine("----------------------------------------------_____:Here status 200");
                        HttpContent content = response.Content;
                        string xjson = await content.ReadAsStringAsync();

                        var json_ = JsonConvert.DeserializeObject<List<Paquetes>>(xjson);
                        PaqueteList.ItemsSource = json_;
                        break;
                }
            }
            catch (Exception ex) {
                await DisplayAlert("", "" + ex.ToString(), "ok");
                return;
            }
        }

        private async void BtnSelPaquete_Clicked(object sender, EventArgs e)
        {
            NavigationPage page = App.Current.MainPage as NavigationPage;
            page.BarBackgroundColor = Color.Beige;
            page.BarTextColor = Color.Black;
            await Navigation.PushAsync(new Planes());
        }
    }
}