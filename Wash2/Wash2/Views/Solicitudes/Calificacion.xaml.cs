using LaavorRatingConception;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Wash2.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wash2.Views.Solicitudes
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Calificacion : ContentPage
	{
        public int voto;
		public Calificacion (int id_s)
		{
			InitializeComponent ();
            _ = GetInfoCalificacion(id_s);
        }

        public async Task GetInfoCalificacion(int id)
        {

            HttpClient client = new HttpClient();

            var url = "http://www.washdryapp.com/app/public/solicitud/cliente/" + id;
            try
            {
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
                        var json_ = JsonConvert.DeserializeObject<List<Solicitud>>(xjson);
                        img_usuario.Source = json_[0].foto;
                        Lbl_nombre.Text = json_[0].nombre;
                        var idS = json_[0].id_solicitud;
                        Id_solicitud.Text = Convert.ToString(idS);
                        break;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("", "" + ex.ToString(), "ok");
                return;
            }
        }

        private async void Btn_EnviarCal_Clicked(object sender, EventArgs e)
        {
            var idS = Id_solicitud.Text;
            var cal = ratingStar;
            var com = Comentario.Text;
            var httpClient = new HttpClient();
            var url = "http://www.washdryapp.com/app/public/cliente/califica";
            var value_check = new Dictionary<string, string>
                {
                    {"id_solicitud", idS},
                    {"calificacion", voto.ToString()},
                    {"comentario", com}
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
                    string xjson = await responseMsg.Content.ReadAsStringAsync();
                    await DisplayAlert("Success", "Calificación enviada", "ok");
                    await Navigation.PushAsync(new Wash2.Views.Solicitudes.SolicitaSolicitud());
                    break;
                case System.Net.HttpStatusCode.Unauthorized:
                    await DisplayAlert("error", "yeah status 401 Unauthorized", "ok");
                    break;
            }
            await DisplayAlert("", "ID"+idS+" <-> Calif "+voto+" <-> "+com, "ok");
        }

        private void RatingStar_Voted(object sender, EventArgs e)
        {
            RatingConception rating = (RatingConception)sender;
            int index = rating.IndexVoted;
            int value = rating.Value;
            voto = value;
        }
    }
}