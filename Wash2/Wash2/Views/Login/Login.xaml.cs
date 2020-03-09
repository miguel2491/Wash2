using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Wash2.Menu;

namespace Wash2.Views.Login
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
		public Login ()
		{
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent ();
		}

        private async void Iniciar_Sesion_Clicked(object sender, EventArgs e)
        {
            try
            {
                var usuario = Email_login.ToString();
                var pass = Pass.ToString();

                StringContent Usuario = new StringContent(usuario);
                StringContent Pasw = new StringContent(pass);

                var content = new MultipartFormDataContent();
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                content.Add(Usuario, "usuario");
                content.Add(Pasw, "pass");

                
                var httpClient = new System.Net.Http.HttpClient();
                var url = "http://www.washdryapp.com/app/public/washer/login";
                var responseMsg = await httpClient.PostAsync(url, content);
                switch (responseMsg.StatusCode)
                {
                    case System.Net.HttpStatusCode.InternalServerError:
                        await DisplayAlert("ErroR", "error status 500 InternalServerError", "ok");
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
                        await DisplayAlert("Aviso", "Correcto : " + xjson, "ok");
                        Application.Current.MainPage = new MainPage();
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
            catch (Exception ex){
                await DisplayAlert("Error EX", "Error : " + ex.ToString(), "OK");
            }
        }
        private async void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            NavigationPage page = App.Current.MainPage as NavigationPage;
            page.BarBackgroundColor = Color.Beige;
            page.BarTextColor = Color.Black;
            await Navigation.PushAsync(new Registro());
        }
        private async void RecuperarPass(object sender, EventArgs args)
        {
            NavigationPage page = App.Current.MainPage as NavigationPage;
            page.BarBackgroundColor = Color.Beige;
            page.BarTextColor = Color.Black;
            await Navigation.PushAsync(new RecuperarPass());
        }
    }
}