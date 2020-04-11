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

namespace Wash2.Views.Pagos
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Pagos : ContentPage
	{
        DatePicker datePicker;
        DateTime newDate;
        DateTime oldDate;
        public Usuario user;
        private UserDB userdb;

        public Pagos ()
		{
			InitializeComponent ();
            userdb = new UserDB();
            var user_wash = userdb.GetMembers().ToList();
            var id_washer = user_wash[0].idWasher;
            Lbl_Usuario.Text = Convert.ToString(id_washer);
            _ = GetPagos();
        }

        public async Task GetPagos()
        {
            var id_washer = Lbl_Usuario.Text;
            var fca = Fca.Date.ToString("yyyy-MM-dd");
            try
            {
                HttpClient client = new HttpClient();
                
                var value_check = new Dictionary<string, string>
                         {
                            {"id_washer", Convert.ToString(id_washer)},
                            {"fecha" , fca }
                         };
                var url = "http://www.washdryapp.com/app/public/pagos/listaPagos";
                var content = new FormUrlEncodedContent(value_check);
                var response = await client.PostAsync(url, content);
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.InternalServerError:
                        Console.WriteLine("----------------------------------------------_____:Here status 500");
                        break;
                    case System.Net.HttpStatusCode.OK:
                        Console.WriteLine("----------------------------------------------_____:Here status 200");
                        HttpContent content2 = response.Content;
                        string xjson = await content2.ReadAsStringAsync();
                        if (xjson.Count() <= 0 || xjson.Length <= 2)
                        {
                            Lbl_ganancias.Text = "0";
                            Lbl_cobros.Text = "0";
                            Lbl_cobroTotal.Text = "0";
                        }
                        else
                        {
                            var json_ = JsonConvert.DeserializeObject<List<Pago>>(xjson);
                            var ganancia = json_[0].ganancias;
                            //if (ganancia == null) { ganancia = 0; }
                            ganancia = ganancia * 25 / 100;
                            Lbl_ganancias.Text = Convert.ToString(json_[0].ganancias);
                            Lbl_cobros.Text = ganancia.ToString();
                            Lbl_cobroTotal.Text = ganancia.ToString();
                        }
                        
                        //ListPagos.ItemsSource = json_;
                        break;
                }
                
            }
            catch (Exception ex)
            {
                await DisplayAlert("", "" + ex.ToString(), "ok");
                return;
            }
        }

            private async void BtnInfo_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("INFORMACIÓN", "Ganas el 25% de cada lavada Cada viernes recibiras tu dinero registra tu tarjeta", "ACEPTAR");
        }

        private void Fca_DateSelected(object sender, DateChangedEventArgs e)
        {
            newDate = e.NewDate;
            oldDate = e.OldDate;
            Lbl_ganancias.Text = "0";
            Lbl_cobros.Text = "0";
            Lbl_cobroTotal.Text = "0";
            _ = GetPagos();
        }
    }
}