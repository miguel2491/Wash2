using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Wash2.Models;
using Wash2.Views.Solicitudes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wash2.Views.Estado
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Confirmar : ContentPage
	{
        public int idS;
		public Confirmar (int id_s)
		{
			InitializeComponent ();
            idS = id_s;
		}

        private async void BtnConfirma_Clicked(object sender, EventArgs e)
        {
            activityConfirmar.IsRunning = true;
            var httpClient = new HttpClient();
            var url = "http://www.washdryapp.com/app/public/solicitud/confirmaLlegada";
            var value_check = new Dictionary<string, string>
                {
                    {"id_solicitud", Convert.ToString(idS) }
                };
            var content = new FormUrlEncodedContent(value_check);
            var responseMsg = await httpClient.PostAsync(url, content);

            switch (responseMsg.StatusCode)
            {
                case System.Net.HttpStatusCode.InternalServerError:
                    await DisplayAlert("error", "error status 500 InternalServerError", "ok");
                    break;
                case System.Net.HttpStatusCode.BadRequest:
                    HttpContent content2 = responseMsg.Content;
                    string json = await content2.ReadAsStringAsync();
                    if (json == "{\"status\":\"fail\"}")
                    {
                        await DisplayAlert("¡Ups!", "Solicitud Cancelada por el Cliente", "Aceptar");
                    }
                    else {
                        await DisplayAlert("¡Error!", "Ocurrio un error, vuelva a intentarlo mas tarde", "Aceptar");
                    }
                    await Navigation.PushAsync(new Wash2.Views.Solicitudes.SolicitaSolicitud());
                    break;
                case System.Net.HttpStatusCode.Forbidden:
                    await DisplayAlert("error", "error status 403  ", "ok");
                    break;
                case System.Net.HttpStatusCode.NotFound:
                    await DisplayAlert("error", "error status 404  ", "ok");
                    break;
                case System.Net.HttpStatusCode.OK:
                    await Navigation.PushAsync(new Wash2.Views.Estado.EdoIndi(idS));
                    break;
                case System.Net.HttpStatusCode.Unauthorized:
                    await DisplayAlert("error", "yeah status 401 Unauthorized", "ok");
                    break;
            }
            activityConfirmar.IsRunning = false;
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Aviso", "Realmente quieres regresar, cancelaras el proceso actual?", "Si", "No");
                if (result) {
                    await ((MainPage)App.Current.MainPage).Detail.Navigation.PushAsync(new SolicitaSolicitud());
                }
            });
            return true;
        }
    }
}