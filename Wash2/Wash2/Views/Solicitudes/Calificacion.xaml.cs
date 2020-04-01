using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wash2.Views.Solicitudes
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Calificacion : ContentPage
	{
		public Calificacion (int id_s)
		{
			InitializeComponent ();
            _ = GetInfoCalificacion(id_s);
        }

        public async Task GetInfoCalificacion(int id)
        {

            HttpClient client = new HttpClient();

            var url = "http://www.washdryapp.com/app/public/paquete/individual/" + id;
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
                        //var json_ = JsonConvert.DeserializeObject<List<PaqueteDetalle>>(xjson);
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
}