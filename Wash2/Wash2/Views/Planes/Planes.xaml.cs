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

namespace Wash2.Views.Planes
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Planes : ContentPage
	{
        public string id_paquete;

		public Planes (int id_paquete)
		{
			InitializeComponent ();
            _ = GetInfoPaquete(id_paquete);
            
        }

        public async Task GetInfoPaquete(int id_paquete)
        {
            await DisplayAlert("error", "ID" + id_paquete, "ok");
            HttpClient client = new HttpClient();

            var url = "http://www.washdryapp.com/app/public/paquete/individual/"+id_paquete;
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

                        var json_ = JsonConvert.DeserializeObject<List<PaqueteDetalle>>(xjson);
                        PaqueteDetalleList.ItemsSource = json_;
                        break;
                }
            }
            catch (Exception ex) {
                await DisplayAlert("", "" + ex.ToString(), "ok");
                return;
            }
        }

        private void BtnElegirPaquete_Clicked(object sender, EventArgs e)
        {

        }
    }
}