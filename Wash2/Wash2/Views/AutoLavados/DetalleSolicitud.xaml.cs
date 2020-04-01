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

namespace Wash2.Views.AutoLavados
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetalleSolicitud : ContentPage
	{
		public DetalleSolicitud (int id)
		{
			InitializeComponent ();
            _ = GetSolicitud(id);
        }

        public async Task GetSolicitud(int id)
        {
            HttpClient client = new HttpClient();

            var url = "http://www.washdryapp.com/app/public/solicitud/lista_solicitud/" + id;
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
                        var json_ = JsonConvert.DeserializeObject<List<Solicitud>>(xjson);
                        ListSolicitud.ItemsSource = json_;
                        
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

        private async void Btn_Aceptar_Clicked(object sender, EventArgs e)
        {
            /*UPDATE*/
            App.MasterD.IsPresented = false;
            await((MainPage)App.Current.MainPage).Detail.Navigation.PushAsync(new AutosLavados());
        }
    }
}