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

namespace Wash2.Views.AutoLavados
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetalleSolicitud : ContentPage
	{
        public int voto;

        public DetalleSolicitud (int id)
		{
			InitializeComponent ();
            Lbl_solicitud.Text = Convert.ToString(id);
            _ = GetSolicitud(id);
        }

        public async Task GetSolicitud(int id)
        {
            HttpClient client = new HttpClient();

            var url = "http://www.washdryapp.com/app/public/solicitud/washer_solicitud/" + id;
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
                        //ListSolicitud.ItemsSource = json_;
                        Lbl_fca.Text = Convert.ToString(json_[0].fecha);
                        Lbl_modelo.Text = json_[0].modelo;
                        Lbl_placas.Text = json_[0].placas;
                        Lbl_precio.Text = json_[0].precio;
                        Lbl_ann.Text = json_[0].ann;
                        Lbl_usuario.Text = json_[0].usuario;
                        foto.Source = json_[0].foto;
                        ratingStar.InitialValue = json_[0].calificacion;
                        Lbl_paquete.Text = json_[0].paquete;
                        r_calif.InitialValue = json_[0].calificacion;
                        Comentarios.Text = json_[0].comentario;
                        Lbl_subtotal.Text = json_[0].monto;
                        Lbl_tarifa.Text = json_[0].precio;
                        var total = Convert.ToDecimal(json_[0].monto) + Convert.ToDecimal(json_[0].precio);
                        Lbl_totalT.Text = Convert.ToString(total);
                        //SolicitudList.ItemsSource = json_;
                        break;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("", "" + ex.ToString(), "ok");
                return;
            }
        }

        private async void Btn_Aceptar_Clicked(object sender, EventArgs e)
        {
            var id = Lbl_solicitud.Text;
            var comentario = Comentarios.Text;
            /**/
            var httpClient = new HttpClient();
            var url = "http://www.washdryapp.com/app/public/solicitud/detalle";

            var value_check = new Dictionary<string, string>
                         {
                            {"id_solicitud", id},
                            {"calificacion" , Convert.ToString(voto) },
                            {"comentario", comentario }
                         };

            var content = new FormUrlEncodedContent(value_check);
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
                    try
                    {
                        HttpContent resp_content = responseMsg.Content;
                        var json = await resp_content.ReadAsStringAsync();
                        App.MasterD.IsPresented = false;
                        await ((MainPage)App.Current.MainPage).Detail.Navigation.PushAsync(new AutosLavados());
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("", "" + ex.ToString(), "ok");
                    }

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

        private void R_calif_Voted(object sender, EventArgs e)
        {
            RatingConception rating = (RatingConception)sender;
            int index = rating.IndexVoted;
            int value = rating.Value;
            voto = value;
        }
    }
}