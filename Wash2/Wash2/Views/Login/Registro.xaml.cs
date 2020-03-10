using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Wash2.Views.Planes;

namespace Wash2.Views.Login
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Registro : ContentPage
	{
		public Registro ()
		{
			InitializeComponent ();
            Title = "WASH DRY";
		}
        private async void Registrar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var nombre = Nombre.ToString();
                var apps = Apps.ToString();

                StringContent Nombres = new StringContent(nombre);
                StringContent Apellido = new StringContent(apps);

                var content = new MultipartFormDataContent();

                content.Add(Nombres, "nombre");
                content.Add(Apellido, "apps");
                content.Add(Apellido, "correo");
                content.Add(Apellido, "password");

                var httpClient = new System.Net.Http.HttpClient();
                var url = "http://www.washdryapp.com/app/public/washer/guardar";
                var responseMsg = await httpClient.PostAsync(url, content);

                switch (responseMsg.StatusCode)
                {
                    case System.Net.HttpStatusCode.InternalServerError:
                        await DisplayAlert("error", "error status 500 InternalServerError", "ok");
                        break;
                    case System.Net.HttpStatusCode.BadRequest:
                        await DisplayAlert("error", "error status 400 Unauthorized", "ok");
                        break;
                    case System.Net.HttpStatusCode.Forbidden:
                        await DisplayAlert("error", "error status 403  ", "ok");
                        break;
                    case System.Net.HttpStatusCode.NotFound:
                        await DisplayAlert("error", "error status 404  ", "ok");
                        break;
                    case System.Net.HttpStatusCode.OK:
                        string xjson = await responseMsg.Content.ReadAsStringAsync();
                        await DisplayAlert("error", "Correcto : " + xjson, "ok");
                        break;
                    case System.Net.HttpStatusCode.RequestEntityTooLarge:
                        await DisplayAlert("error", "error status 413  ", "ok");
                        break;
                    case System.Net.HttpStatusCode.RequestTimeout:
                        await DisplayAlert("error", "error status 408  ", "ok");
                        break;
                    case System.Net.HttpStatusCode.Unauthorized:
                        await DisplayAlert("error", "yeah status 401 Unauthorized", "ok");
                        break;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Error : " + ex.ToString(), "OK");
            }
            Application.Current.MainPage = new NavigationPage(new Login());
        }
        private async void Paquetes_Clicked(object sender, EventArgs e)
        {
            NavigationPage page = App.Current.MainPage as NavigationPage;
            page.BarBackgroundColor = Color.Beige;
            page.BarTextColor = Color.Black;
            await Navigation.PushAsync(new Principal());
        }

    }
}