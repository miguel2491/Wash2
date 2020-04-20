using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
                    await DisplayAlert("error", "error status 400 Unauthorized", "ok");
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
        }
    }
}