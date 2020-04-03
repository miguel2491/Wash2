using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Wash2.Models;
using Wash2.Views.Solicitudes;
using Wash2.SQLiteDB;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wash2.Views.Solicitudes
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SolicitaSolicitud : ContentPage
	{
        public Usuario user;
        private UserDB userdb;

        public SolicitaSolicitud ()
		{
			InitializeComponent ();
            userdb = new UserDB();
            var user_exist = userdb.GetMembers().ToList();
            var idW = user_exist[0].idWasher;
            _ = GetAutos(idW);
        }

        public async Task GetAutos(int id_washer)
        {
            HttpClient client = new HttpClient();
            var url = "http://www.washdryapp.com/app/public/washer/get_solicitud/" + id_washer;
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
                        if (xjson.Count() <= 0 || xjson.Length <= 2)
                        {
                            lblMainlavados.Text = "NO TIENES SOLICITUDES DE LAVADO AÚN";
                        }
                        else {
                            var json_ = JsonConvert.DeserializeObject<List<Solicitud>>(xjson);
                            ListSolicitudes.ItemsSource = json_;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("", "" + ex.ToString(), "ok");
                return;
            }
        }

        private async void ListSolicitud_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var idP = e.Item as Solicitud;
            var id_s = idP.id_solicitud;
            bool answer = await DisplayAlert("Solicitud", "Aceptar Solicitud:"+idP.id_solicitud, "Si", "No");
            if (answer == true) {
                var httpClient = new HttpClient();
                var url = "http://www.washdryapp.com/app/public/solicitud/aceptar_solicitud";
                var value_check = new Dictionary<string, string>
                {
                    {"id", Convert.ToString(id_s)},
                    {"status" , "2" }
                };
                try
                {
                    var content = new FormUrlEncodedContent(value_check);
                    var responseMsg = await httpClient.PostAsync(url, content);
                    switch (responseMsg.StatusCode)
                    {
                        case System.Net.HttpStatusCode.InternalServerError:
                            Console.WriteLine("----------------------------------------------_____:Here status 500");
                            break;
                        case System.Net.HttpStatusCode.OK:
                            Console.WriteLine("----------------------------------------------_____:Here status 200");
                            await DisplayAlert("Correcto", "Solicitud Aceptada", "ok");
                            getOut(id_s);
                            
                            break;
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("", "" + ex.ToString(), "ok");
                    return;
                }
            }
            
        }

        public async void getOut(int id_s)
        {
            await Navigation.PushAsync(new Wash2.Views.Estado.EdoIndi(id_s));
        }
        
    }
}