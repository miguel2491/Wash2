using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wash2.Views.Login
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RecuperarPass : ContentPage
	{
		public RecuperarPass ()
		{
			InitializeComponent ();
		}

        private async void Recuperar_Pass_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Alert", "You have been alerted", "OK");
            try {
                var email = Correo_recupera.ToString();
                StringContent Emails = new StringContent(email);

                var content = new MultipartFormDataContent();

                content.Add(Emails, "correo");
                var httpClient = new System.Net.Http.HttpClient();
                var url = "http://www.washdryapp.com/app/public/washer/recupera_pass";
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
            catch (Exception ex) {
                await DisplayAlert("Error", "Error : " + ex.ToString(), "OK");
            }
            Application.Current.MainPage = new NavigationPage(new Login());
        }
        
    }
}