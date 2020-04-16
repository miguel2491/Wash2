using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
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
using Wash2.SQLiteDB;

namespace Wash2.Views.Estado
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EdoIndi : ContentPage
	{
        private MediaFile _image;
        public string filename;
        public string foto_;
        private int idx;
        public Usuario user;
        private UserDB userdb;

        public EdoIndi (int id_s)
		{
			InitializeComponent ();
            
            //_ = GetInfoCalificacion(id_s);
            _ = CurrentLocation(id_s);
            
            Lbl_idSol.Text = Convert.ToString(id_s);
            idx = id_s;
            userdb = new UserDB();
            var user_ = userdb.GetMembers().ToList();
            foto_ = user_[0].email;
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
                        Lbl_yyyy.Text = json_[0].ann;
                        img_perfil.Source = json_[0].foto;
                        img_a.Source = json_[0].foto_washer;
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
            double zoomLevel = 16;
            double latlongDegrees = 360 / (Math.Pow(2, zoomLevel));
            MapView.MoveToRegion(
            MapSpan.FromCenterAndRadius(
                new Position(pos.Latitude, pos.Longitude), Distance.FromMiles(.3)    
                )
            );


                

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
                Directory = "Auto_",
                Name = foto_+".jpg"
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
            /*Imagen*/
            var imgExist = img_a.Source;
            if (imgExist == null)
            {
                //GUARDAR IMAGEN
                var content1 = new MultipartFormDataContent();
                content1.Add(new StreamContent(_image.GetStream()), "\"file\"", $"\"{_image.Path}\"");

                var httpClient1 = new System.Net.Http.HttpClient();
                httpClient1.BaseAddress = new Uri("http://www.washdryapp.com");
                var url1 = "http://www.washdryapp.com/oficial/ImagenesAutos.php";
                var responseMsg1 = await httpClient1.PostAsync(url1, content1);
                var remotePath = await responseMsg1.Content.ReadAsStringAsync();
                filename = remotePath;
                //*****Solicitud******
                var httpClient = new HttpClient();
                var url = "http://www.washdryapp.com/app/public/solicitud/solicitud_img";
                var value_check = new Dictionary<string, string>
                {
                    {"id_solicitud", Convert.ToString(idx) },
                    {"foto", filename}
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
                        await DisplayAlert("Success", "Se agrego Correctamente ", "ok");
                        break;
                    case System.Net.HttpStatusCode.Unauthorized:
                        await DisplayAlert("error", "yeah status 401 Unauthorized", "ok");
                        break;
                }
                //**************
            }
            else {

            }
            
            await ((MainPage)App.Current.MainPage).Detail.Navigation.PushAsync(new Calificacion(idx));
        }
    }
}