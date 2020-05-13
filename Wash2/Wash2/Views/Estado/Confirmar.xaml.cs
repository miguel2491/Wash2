using Newtonsoft.Json;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Wash2.Models;
using Wash2.Views.Solicitudes;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using System.Threading;

namespace Wash2.Views.Estado
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Confirmar : ContentPage
	{
        public int idS;
		public Confirmar (int id_s)
		{
			InitializeComponent ();
            _ = CurrentLocation();
            idS = id_s;
            Device.StartTimer(TimeSpan.FromMinutes(1), () => //Will start after 1 min
            {
                Task.Run(() =>
                {
                    //Mapx.IsVisible = false;
                    Console.WriteLine("CADA MINUTE");
                    // do something with time...
                    
                });

                return false; // To repeat timer,always return true.If you want to stop the timer,return false
            });
        }

        public async Task CurrentLocation()
        {
            
            Mapx.Pins.Clear();
            Mapx.IsVisible = true;
            var pos = await CrossGeolocator.Current.GetPositionAsync();

            Mapx.MoveToRegion(
            MapSpan.FromCenterAndRadius(
            new Position(pos.Latitude, pos.Longitude), Distance.FromMiles(1)));
            HttpClient client = new HttpClient();
            var url = "http://www.washdryapp.com/app/public/solicitud/lista_solicitud/" + idS;
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
                        var xjson = await content.ReadAsStringAsync();
                        var json_ = JsonConvert.DeserializeObject<List<Solicitud>>(xjson);
                        var lat = Convert.ToDouble(json_[0].latitud);
                        var lon = Convert.ToDouble(json_[0].longitud);
                        //lat = Convert.ToDecimal(lat);
                        var pin = new Pin
                        {
                            Type = PinType.Place,
                            Position = new Position(lon, lat),
                            Label = "Mi ubicacion",
                            Address = "  usted se encuentra aqui",

                        };
                        Mapx.Pins.Add(pin);
                        /*RUTA DRAW*/
                        Map map = new Map { };
                        Polyline polyline = new Polyline
                        {
                            StrokeColor = Color.Blue,
                            StrokeWidth = 12,
                            Geopath =
                            {
                                new Position(pos.Latitude, pos.Longitude),
                                new Position(lon, lat)
                            }
                        };
                        Mapx.MapElements.Add(polyline);
                        //
                        break;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("", "" + ex.ToString(), "ok");
                return;
            }

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