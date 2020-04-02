using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Wash2.Views.Planes;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;
using Wash2.SQLiteDB;
using Wash2.Models;

namespace Wash2.Views.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registro : ContentPage
    {
        public Registros regs;
        public Usuario user;
        private RegistrosDB regsdb;
        private UserDB userdb;

        public Registro()
        {
            InitializeComponent();
            Title = "WASH DRY";
            regsdb = new RegistrosDB();
            //PropertyMaximumDate = DateTime.Now;
            userdb = new UserDB();
            var user_token = userdb.GetMembers().ToList();
            var regsW = new Registros();
            var regs_exist = regsdb.GetRegistro();
            int RowCount = 0;
            int regcount = regs_exist.Count();
            RowCount = Convert.ToInt32(regcount);
            var token = user_token[0].token;
            if (RowCount > 1)
            {
                regsdb.DeleteAllReg();
                regsW.paquete = 1;
                regsW.tokenReg = user_token[0].token;
                regsdb.AddRegs(regsW);
            }
            else if (RowCount == 1)
            {
                var reg_exist = regsdb.GetRegistro().ToList();
                Console.WriteLine("ID es =>" + reg_exist[0].id + " <-> " + reg_exist[0].paquete);
                var idPaq = reg_exist[0].paquete;
                var nombre = reg_exist[0].nombre;
                var app = reg_exist[0].app;
                var apm = reg_exist[0].apm;
                var fca = Convert.ToDateTime(reg_exist[0].fecha);
                var tel = reg_exist[0].telefono;
                var correo = reg_exist[0].correo;
                var passw = reg_exist[0].password;
                IdPaquete.Text = idPaq.ToString();
                Nombre.Text = nombre;
                Appaterno.Text = app;
                Apmaterno.Text = apm;
                Fca_nac.Date = fca;
                Telefono.Text = tel;
                Correo.Text = correo;
                Password.Text = passw;
            }
            else
            {
                regsW.paquete = 1;
                regsW.tokenReg = user_token[0].token;
                regsdb.AddRegs(regsW);
            }
        }
        private async void Registrar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var reg_exist = regsdb.GetRegistro().ToList();
                var paquete = reg_exist[0].paquete;
                
                var nombre = reg_exist[0].nombre;
                var app = reg_exist[0].app;
                var apm = reg_exist[0].apm;
                var fca_nac = reg_exist[0].fecha;
                var telefono = reg_exist[0].telefono;
                var correo = reg_exist[0].correo;
                var password = reg_exist[0].password;
                var confPass = ConfirmaPass.Text;
                var tokens = reg_exist[0].tokenReg;
                if (tokens == null || tokens == "") {
                    userdb = new UserDB();
                    var user_token = userdb.GetMembers().ToList();
                    var tokenUsuario = user_token[0].token;
                    tokens = tokenUsuario;
                }
                if (password == "" || password == null) {
                    password = Password.Text;
                    confPass = ConfirmaPass.Text;
                }
                if (password == confPass)
                {
                    var httpClient = new HttpClient();
                    var url = "http://www.washdryapp.com/app/public/washer/guardar";

                    var value_check = new Dictionary<string, string>
                {
                    {"nombre", nombre },
                    {"app", app},
                    {"apm", apm},
                    {"fecha_nac", fca_nac },
                    {"telefono", telefono },
                    {"correo", correo },
                    {"password", password },
                    {"token", tokens },
                    {"id_paquete", paquete.ToString() }
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
                            regsdb.DeleteAllReg();
                            await DisplayAlert("Success", "Se agrego Correctamente ", "ok");
                            break;
                        case System.Net.HttpStatusCode.Unauthorized:
                            await DisplayAlert("error", "yeah status 401 Unauthorized", "ok");
                            break;
                    }
                }else {
                    await DisplayAlert("error", "Contraseña no coinciden", "ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Error : " + ex.ToString(), "OK");
            }
            Application.Current.MainPage = new NavigationPage(new Login());
        }
        private async void Paquetes_Clicked(object sender, EventArgs e)
        {
            regsdb = new RegistrosDB();
            var reg_exist = regsdb.GetRegistro().ToList();
            var idReg = reg_exist[0].id;
            var nombre = Nombre.Text;
            var app = Appaterno.Text;
            var apm = Apmaterno.Text;
            var fca_nac = Fca_nac.Date.ToString("yyyy-MM-dd");//Fca_nac.Date.ToString();
            var telefono = Telefono.Text;
            var correo = Correo.Text;
            var password = Password.Text;
            regsdb.UpdateAll(idReg, nombre, app, apm, fca_nac.ToString(), telefono, correo, password, 1);

            NavigationPage page = App.Current.MainPage as NavigationPage;
            page.BarBackgroundColor = Color.Beige;
            page.BarTextColor = Color.Black;
            await Navigation.PushAsync(new Principal());
        }

    }
}