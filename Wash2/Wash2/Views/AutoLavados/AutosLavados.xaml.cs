﻿using Newtonsoft.Json;
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
	public partial class AutosLavados : ContentPage
	{
        public int id_usuario;

        public AutosLavados ()
		{

			InitializeComponent ();
            _ = GetAutos(13);
        }

        public async Task GetAutos(int id_usuario)
        {
            HttpClient client = new HttpClient();

            var url = "http://www.washdryapp.com/app/public/washer/get_solicitud/" + id_usuario;
            try
            {
                var response = await client.GetAsync(url);
                //await DisplayAlert("", "A->" + response.StatusCode, "ok");
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

        private async void Descripcion_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DetalleSolicitud());
        }
    }
}