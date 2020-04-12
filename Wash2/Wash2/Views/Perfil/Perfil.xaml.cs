using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Wash2.Views.Planes;
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
        public Registros regs;
        private RegistrosDB regsdb;
        public string imagen_name;
        public string username;
        public string fotoActual;
        private MediaFile _image;

        public Perfil ()
		{
			InitializeComponent ();
            _ = GetUsuario();
        }

        public async Task GetUsuario()
        {
            userdb = new UserDB();
            var user_exist = userdb.GetMembers().ToList();
            var pass = user_exist[0].password;
            var foto = user_exist[0].foto;
            fotoActual = user_exist[0].foto;
            username = user_exist[0].email;
            imgPerfil.Source = foto;
            //imgx.Source = foto;
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
                        var fecha = Convert.ToDateTime(json[0].fecha);
                        Nombre.Text = json[0].nombre;
                        Appaterno.Text = json[0].app;
                        Apmaterno.Text = json[0].apm;
                        Ann_.Date = fecha;
                        Telefono.Text = json[0].telefono;
                        Correo.Text = json[0].email;
                        Contraseña.Text = pass;
                        fotoActual = json[0].foto;
                        IdWasher.Text = Convert.ToString(json[0].idWasher);
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
            
        }
        
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
                Directory = "washer_",
                Name = username+".jpg"
            });
            if (_image == null)
                return;
            // await DisplayAlert("File Location Error", "Error parece que hubo un problema con la camara, confirme espacio en memoria o notifique a sistemas", "OK");
            var xlocal = _image.Path;
            imgx.Source = ImageSource.FromStream(() => {
                return _image.GetStream();
            });
        }

        private async void BtnActualiza_Clicked(object sender, EventArgs e)
        {
            
            //ACTUALIZA IMG
            if (_image != null)
            {
                //GUARDAR IMAGEN
                var content1 = new MultipartFormDataContent();
                content1.Add(new StreamContent(_image.GetStream()), "\"file\"", $"\"{_image.Path}\"");

                var httpClient1 = new System.Net.Http.HttpClient();
                httpClient1.BaseAddress = new Uri("http://www.washdryapp.com");
                var url1 = "http://www.washdryapp.com/oficial/ImagenesPerfil.php";
                var responseMsg1 = await httpClient1.PostAsync(url1, content1);
                var remotePath = await responseMsg1.Content.ReadAsStringAsync();
                imagen_name = "http://washdryapp.com/oficial/Perfiles/" + remotePath;
                //
            }
            else {
                imagen_name = fotoActual;
            }
            //*****************************************
            var httpClient = new HttpClient();
            var url = "http://www.washdryapp.com/app/public/washer/update";

            userdb = new UserDB();
            regsdb = new RegistrosDB();

            var user_token = userdb.GetMembers().ToList();
            var id_usuario = user_token[0].idB;

            var reg_ = regsdb.GetRegistro().ToList();
            

            var nombre = Nombre.Text;
            var app = Appaterno.Text;
            var apm = Apmaterno.Text;
            var fca_nac = Ann_.Date.ToString();
            var telefono = Telefono.Text;
            var correo = Correo.Text;
            var pass = Contraseña.Text;

            var value_check = new Dictionary<string, string>
                {
                    {"id_usuario", id_usuario.ToString() },
                    {"nombre", nombre},
                    {"app", app},
                    {"apm", apm},
                    {"fecha_nac", fca_nac },
                    {"telefono", telefono },
                    {"correo", correo },
                    {"password", pass },
                    {"foto", imagen_name }
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
                    userdb = new UserDB();
                    var user_existe = userdb.GetMembers().ToList();
                    var idUser = user_existe[0].id;

                    var id = id_usuario;
                    var name = Nombre.Text;
                    var nombres = Nombre.Text + " " + Appaterno.Text + " " + Apmaterno.Text;
                    var password = Contraseña.Text;
                    var email = Correo.Text;
                    var idWasher = Convert.ToInt32(IdWasher.Text);
                    var foto = imagen_name;
                    var status = 1;

                    userdb.UpdateMember(idUser, id, name, email, nombres, password, idWasher, foto, status);
                    await DisplayAlert("Success", "Se Actualizo Correctamente ", "ok");

                    break;
                case System.Net.HttpStatusCode.Unauthorized:
                    await DisplayAlert("error", "yeah status 401 Unauthorized", "ok");
                    break;
            }
        }

        private async void BtnPaquete_Clicked(object sender, EventArgs e)
        {
            regsdb = new RegistrosDB();
            var reg_exist = regsdb.GetRegistro().ToList();
            var idReg = reg_exist[0].id;
            var nombre = Nombre.Text;
            var app = Appaterno.Text;
            var apm = Apmaterno.Text;
            var fca_nac = Ann_.Date.ToString();
            var telefono = Telefono.Text;
            var correo = Correo.Text;
            var password = Contraseña.Text;
            regsdb.UpdateAll(idReg, nombre, app, apm, fca_nac, telefono, correo, password, 2);

            await Navigation.PushAsync(new Principal());
        }
    }
}