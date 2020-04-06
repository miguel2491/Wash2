using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using Plugin.Geolocator;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Wash2.Views.Solicitudes;
using System.Net.Http;
using Newtonsoft.Json;
using Wash2.Models;

namespace Wash2.Views.Estado
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EdoIndi : ContentPage
	{
        private MediaFile _image;
        private int idx;
        public EdoIndi (int id_s)
		{
			InitializeComponent ();
            //_ = GetInfoCalificacion(id_s);
            _ = CurrentLocation(id_s);
            idx = id_s;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        public async Task CurrentLocation(int id_s)
        {
            HttpClient client = new HttpClient();
            var url = "http://www.washdryapp.com/app/public/solicitud/lista_solicitud/" + id_s;
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
                        img_perfil.Source = json_[0].foto;
                        Lbl_modelo.Text = json_[0].modelo;
                        Lbl_placa.Text = json_[0].placas;
                        Lbl_nombre.Text = json_[0].nombre + " " + json_[0].app + " " + json_[0].apm;
                        break;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("", "" + ex.ToString(), "ok");
                return;
            }

            var pos = await CrossGeolocator.Current.GetPositionAsync();
            MapView.MoveToRegion(
            MapSpan.FromCenterAndRadius(
            new Position(pos.Latitude, pos.Longitude), Distance.FromMiles(1)));


            var pin = new Pin
            {
                Type = PinType.Place,
                Position = new Position(pos.Latitude, pos.Longitude),
                Label = "Mi ubicacion",
                Address = "  usted se encuentra aqui",

            };
            MapView.Pins.Add(pin);
        }

        private async void BtnFoto_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera soportada.", "OK");
                return;
            }
            _image = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "Edo_" + idx,
                Name = "Edo.jpg"
            });
            if (_image == null)
                return;
            // await DisplayAlert("File Location Error", "Error parece que hubo un problema con la camara, confirme espacio en memoria o notifique a sistemas", "OK");
            var xlocal = _image.Path;
            img_a.Source = ImageSource.FromStream(() => {

                return _image.GetStream();


            });
        }

        private async void BtnCheckOut_Clicked(object sender, EventArgs e)
        {
            await ((MainPage)App.Current.MainPage).Detail.Navigation.PushAsync(new Calificacion(idx));
        }
    }
}