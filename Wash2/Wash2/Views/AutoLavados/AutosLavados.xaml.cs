using Newtonsoft.Json;
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

namespace Wash2.Views.AutoLavados
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AutosLavados : ContentPage
	{
        public int id_usuario;
        public Usuario user;
        private UserDB userdb;

        public AutosLavados ()
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

            var url = "http://www.washdryapp.com/app/public/washer/get_solicitud_lavado/" + id_washer;
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
                        if (xjson.Count() <= 0 || xjson.Length <= 2)
                        {
                            Lbl_autos.IsVisible = true;
                        }
                        else
                        {
                            Lbl_autos.IsVisible = false;
                            var json_ = JsonConvert.DeserializeObject<List<Solicitud>>(xjson);
                            ListAutos.ItemsSource = json_;
                        }
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

        

        private async void ListAutos_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var content = e.Item as Solicitud;
            await Navigation.PushAsync(new DetalleSolicitud(content.id_solicitud));
        }

    }
}