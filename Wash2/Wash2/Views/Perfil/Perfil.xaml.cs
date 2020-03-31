using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Wash2.Models;
using Wash2.SQLiteDB;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wash2.Views.Perfil
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Perfil : ContentPage
	{
        public Usuario user;
        private UserDB userdb;
        
        public Perfil ()
		{
			InitializeComponent ();
            _ = GetUsuario();
        }

        public async Task GetUsuario()
        {
            userdb = new UserDB();
            var user_exist = userdb.GetMembers().ToList();
            HttpClient client = new HttpClient();
            var uri = "http://www.washdryapp.com/app/public/washer/perfil/" + user_exist[0].idB;
            try
            {
                var response = await client.GetAsync(uri);
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.InternalServerError:
                        Console.WriteLine("---EROR 500--");
                        break;
                    case System.Net.HttpStatusCode.OK:
                        HttpContent content = response.Content;
                        var xjson = await content.ReadAsStringAsync();
                        var json = JsonConvert.DeserializeObject<List<Usuario>>(xjson);

                        Nombre.Text = json[0].nombre;
                        Appaterno.Text = json[0].app;
                        Apmaterno.Text = json[0].apm;
                        break;
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, true);

            idx = "1";
        }

        private MediaFile _image;
        private string idx;

        private async void BtnIne_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera soportada.", "OK");
                return;
            }
            _image = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "auto_" + idx,
                Name = "auto.jpg"
            });
            if (_image == null)
                return;
            // await DisplayAlert("File Location Error", "Error parece que hubo un problema con la camara, confirme espacio en memoria o notifique a sistemas", "OK");
            var xlocal = _image.Path;
            imgx.Source = ImageSource.FromStream(() => {

                return _image.GetStream();


            });
        }
    }
}